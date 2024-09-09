using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BasicAuthenticationJWT.Models;
using Microsoft.IdentityModel.Tokens;

namespace BasicAuthenticationJWT.Utils;

public static class ManageToken
{
     public static string CreateToken(IConfiguration configuration, User user)
     {
          var claims = new ClaimsIdentity();
          claims.AddClaim(new Claim(ClaimTypes.Name, user.Username));
          claims.AddClaim(new Claim(ClaimTypes.Role, user.Role.ToString()));

          var jwtKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"] ?? throw new Exception("Invalid JWT key")));
          var signingCredentials = new SigningCredentials(jwtKey, SecurityAlgorithms.HmacSha256);

          var token = new JwtSecurityToken(
               audience: configuration["Jwt:Audience"],
               issuer: configuration["Jwt:Issuer"],
               claims: claims.Claims,
               expires: DateTime.Now.AddMinutes(30),
               signingCredentials: signingCredentials);
          
          return new JwtSecurityTokenHandler().WriteToken(token);
     }
}