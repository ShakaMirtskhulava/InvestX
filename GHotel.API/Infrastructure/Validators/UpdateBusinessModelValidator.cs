using FluentValidation;
using GHotel.API.Models;

namespace GHotel.API.Infrastructure.Validators;

public class UpdateBusinessModelValidator : AbstractValidator<UpdateBusinessModel>
{
    public UpdateBusinessModelValidator()
    {
        RuleFor(x => x.BusinessId).NotEmpty().WithMessage("Business ID is required");
        RuleFor(x => x.Image).NotEmpty().WithMessage("Image is required");
    }
}
