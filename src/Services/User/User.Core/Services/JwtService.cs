using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Auth.Shared;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using User.Core.Abstractions;
using User.Core.Models;

namespace User.Core.Services;

public class JwtService(IOptions<JwtSettings> jwtSettings) : IJwtService
{
    public string CreateAccessToken(ApplicationUser user)
    {
        var jwtSettingsValue = jwtSettings.Value;
        var tokenHandler = new JwtSecurityTokenHandler();
        var securityKey = Encoding.ASCII.GetBytes(jwtSettingsValue.SecurityKey);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Aud, jwtSettingsValue.ValidAudience),
            new(JwtRegisteredClaimNames.Iss, jwtSettingsValue.ValidIssuer),
            new(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
            new(JwtRegisteredClaimNames.NameId, user.Id),
            new(JwtRegisteredClaimNames.Name, user.Name)
        };

        var descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(jwtSettingsValue.ExpiresHours),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(securityKey),
                SecurityAlgorithms.HmacSha256
            )
        };
        
        var accessToken = tokenHandler.CreateToken(descriptor);
        return tokenHandler.WriteToken(accessToken);
    }
    
    public string CreateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

}