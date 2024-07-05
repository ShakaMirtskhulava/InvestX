using GHotel.Application.Models;

namespace GHotel.Application.Services.User;

public interface IUserService
{
    Task<UserResponseModel> Register(UserRequestModel requestModel, CancellationToken cancellationToken);
    Task<UserResponseModel> Login(UserLoginRequestModel requestModel, CancellationToken cancellationToken);
}
