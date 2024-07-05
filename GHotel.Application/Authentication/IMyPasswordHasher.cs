namespace GHotel.Application.Authentication;

public interface IMyPasswordHasher
{
    string GenerateHash(string password);
    bool VerifyHash(string hash, string password);
}
