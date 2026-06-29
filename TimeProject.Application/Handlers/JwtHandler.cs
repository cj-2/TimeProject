using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TimeProject.Application.Dtos.Auths;
using TimeProject.Application.Interfaces.Handlers;
using TimeProject.Domain.Entities;
using TimeProject.Domain.Entities.Enums;
using TimeProject.Infrastructure.Settings;

namespace TimeProject.Application.Handlers;

public class JwtHandler(JwtSettings jwtSettings) : IJwtHandler
{
    public JwtDto Generate(IUser user)
    {
        var subject = new ClaimsIdentity([
            new Claim("Id", user.UserId.ToString()),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.UserRole.ToString()),
            new Claim("IsAdmin", user.UserRole == UserRoleType.Admin ? "True" : "False"),
            new Claim("IsActive", user.IsActive.ToString())
        ]);

        var expires = DateTime.UtcNow.AddHours(jwtSettings.ExpiresAt).ToUniversalTime();

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
            SecurityAlgorithms.HmacSha256Signature
        );

        var descriptor = new SecurityTokenDescriptor
        {
            Subject = subject,
            Expires = expires,
            SigningCredentials = signingCredentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(descriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return new JwtDto
        {
            Token = tokenString,
            ValidFrom = token.ValidFrom,
            ValidTo = token.ValidTo
        };
    }
}