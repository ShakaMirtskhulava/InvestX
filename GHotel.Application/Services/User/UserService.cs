using GHotel.Application.Authentication;
using GHotel.Application.Exceptions;
using GHotel.Application.Models;
using GHotel.Application.Repositories;
using Mapster;

namespace GHotel.Application.Services.User;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMyPasswordHasher _myPasswordHasher;

    public UserService(IUserRepository userRepository, IMyPasswordHasher myPasswordHasher)
    {
        _userRepository = userRepository;
        _myPasswordHasher = myPasswordHasher;
    }

    public async Task<UserResponseModel> Register(UserRequestModel requestModel, CancellationToken cancellationToken)
    {
        var user = requestModel.Adapt<GHotel.Domain.Entities.User>();
        user.PasswordHash = _myPasswordHasher.GenerateHash(requestModel.Password);
        user = await _userRepository.Create(user, cancellationToken).ConfigureAwait(false);

        return user.Adapt<UserResponseModel>();
    }

    public async Task<UserResponseModel> Login(UserLoginRequestModel requestModel, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmail(requestModel.Email, cancellationToken).ConfigureAwait(false);
        if (user == null)
            throw new NotFoundException("User is not found");
        if (!_myPasswordHasher.VerifyHash(user.PasswordHash, requestModel.Password))
            throw new Exception("Invalid password");

        return user.Adapt<UserResponseModel>();
    }

}
