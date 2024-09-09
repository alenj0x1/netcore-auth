using BasicAuthenticationJWT.Models.Requests;
using BasicAuthenticationJWT.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasicAuthenticationJWT.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IConfiguration configuration, UsersCache usersCache) : Controller
{
    private readonly IConfiguration _config = configuration;
    private readonly UsersCache _users = usersCache;
    
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest model)
    {
        if (string.IsNullOrWhiteSpace(model.Username)) throw new Exception("Username is required");
        if (string.IsNullOrWhiteSpace(model.Password)) throw new Exception("Password is required");

        var gtUser = _users.Get(model.Username) ?? throw new Exception($"User {model.Username} not found");
        
        if (gtUser.Password != model.Password) throw new Exception("Passwords do not match");
        
        var token = ManageToken.CreateToken(_config, gtUser);
        return Ok(token);
    }
} 