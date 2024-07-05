namespace GHotel.API.Infrastructure.Authentication;

public interface IJWTProvider
{
    public string GenerateJWT(string id, string email);
}
