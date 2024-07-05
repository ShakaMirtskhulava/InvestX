using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GHotel.API.Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace GHotel.API.Infrastructure.Authentication;

public class JWTProvider : IJWTProvider
{
    private readonly IOptions<JWTConfiguration> _options;

    public JWTProvider(IOptions<JWTConfiguration> options)
    {
        _options = options;
    }

    public string GenerateJWT(string personalNumber, string email)
    {
        var handler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_options.Value.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, personalNumber),
                new Claim(ClaimTypes.Email, email)
            }),
            Expires = DateTime.UtcNow.AddMinutes(_options.Value.ExpirationTimeInMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _options.Value.Issuer,
            Audience = _options.Value.Audience
        };

        var token = handler.CreateToken(tokenDescriptor);
        return handler.WriteToken(token);
    }
}
