namespace GHotel.API.Infrastructure.Configuration;

#pragma warning disable CS8618

public class JWTConfiguration
{
    public string Secret { get; set; }
    public int ExpirationTimeInMinutes { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
}
