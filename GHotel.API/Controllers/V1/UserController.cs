using Asp.Versioning;
using GHotel.API.Infrastructure.Authentication;
using GHotel.Application.Models;
using GHotel.Application.Services.User;
using Microsoft.AspNetCore.Mvc;

namespace GHotel.API.Controllers.V1;

[ApiController]
[Route("v{version:apiversion}/[controller]")]
[ApiVersion(1)]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserService _userService;
    private readonly IJWTProvider _jwtProvider;

    public UserController(ILogger<UserController> logger, IUserService userService, IJWTProvider jwtProvider)
    {
        _logger = logger;
        _userService = userService;
        _jwtProvider = jwtProvider;
    }

    [HttpPost]
    public async Task<UserResponseModel> Register(UserRequestModel requestModel, CancellationToken cancellationToken)
    {
        var userResponseModel = await _userService.Register(requestModel, cancellationToken).ConfigureAwait(false);
        return userResponseModel;
    }

    [HttpPost("Login")]
    public async Task<string> Login(UserLoginRequestModel requestModel,CancellationToken cancellationToken)
    {
        var userResponseModel = await _userService.Login(requestModel, cancellationToken).ConfigureAwait(false);
        var jwt = _jwtProvider.GenerateJWT(userResponseModel.PersonalNumber, userResponseModel.Email);
        return jwt;
    }
}
