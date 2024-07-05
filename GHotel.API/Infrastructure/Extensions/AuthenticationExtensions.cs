using System.Text;
using GHotel.API.Infrastructure.Authentication;
using GHotel.API.Infrastructure.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace GHotel.API.Infrastructure.Extensions;

public static class AuthenticationExtensions
{
    public static IServiceCollection ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IJWTProvider, JWTProvider>();
        services.Configure<JWTConfiguration>(configuration.GetSection(nameof(JWTConfiguration)));

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = configuration["JWTConfiguration:Issuer"],
                ValidAudience = configuration["JWTConfiguration:Audience"],
                ValidateIssuer = true,
                ValidateAudience = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTConfiguration:Secret"]))
            };
        });

        return services;
    }
}
