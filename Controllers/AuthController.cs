using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace M223PunchclockDotnet.Controllers;

[Route("auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly RSA privateKey;

    public AuthController()
    {
        privateKey = RSA.Create();
        privateKey.ImportFromPem(System.IO.File.ReadAllText("Keys/private.key"));
    }

    [HttpPost("token")]
    public IActionResult GenerateToken()
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = new RsaSecurityKey(privateKey);

        // Claims hinzufügen
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, "TestUser"),
            new Claim(ClaimTypes.Role, "Admin")
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.RsaSha256)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return Ok(new
        {
            Token = tokenHandler.WriteToken(token)
        });
    }
}