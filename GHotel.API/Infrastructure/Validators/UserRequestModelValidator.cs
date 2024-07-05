using FluentValidation;
using GHotel.Application.Models;

namespace GHotel.API.Infrastructure.Validators;

public class UserRequestModelValidator : AbstractValidator<UserRequestModel>
{
    public UserRequestModelValidator()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
        RuleFor(x => x.PersonalNumber).NotEmpty().WithMessage("Personal number is required");
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("Personal number is required");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Personal number is required");
        RuleFor(x => x.BirthDate).LessThan(DateTime.Now).WithMessage("Birth date must be in the past");
    }
}
