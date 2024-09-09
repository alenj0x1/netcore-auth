using BasicAuthenticationJWT.Models;
using BasicAuthenticationJWT.Models.Requests;
using BasicAuthenticationJWT.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasicAuthenticationJWT.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(UsersCache usersCache) : Controller
{
    private readonly UsersCache _users = usersCache;
    
    [Authorize(Roles = "Admin")] // Only users with "Admin" role
    [HttpPost("CreateUser")]
    public IActionResult CreateUser([FromBody] CreateUserRequest model)
    {
        var crtUser = new User()
        {
            Username = model.Username,
            Password = model.Password
        };

        _users.Add(crtUser);
        
        return Ok(crtUser);
    }
    
    [Authorize] // Any user authenticed
    [HttpGet("")]
    public IActionResult GetUser()
    {
        return Ok(_users.Get());
    }
}