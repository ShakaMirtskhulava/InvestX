using FluentValidation;
using GHotel.Application.Models;

namespace GHotel.API.Infrastructure.Validators;

public class UserLoginRequestModelValidator : AbstractValidator<UserLoginRequestModel>
{
    public UserLoginRequestModelValidator()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
    }
}   
