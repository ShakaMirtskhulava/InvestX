using FluentValidation;
using GHotel.API.Models;

namespace GHotel.API.Infrastructure.Validators;

public class UpdateProjectModelValidator : AbstractValidator<UpdateProjectModel>
{
    public UpdateProjectModelValidator()
    {
        RuleFor(x => x.ProjectName).NotEmpty().WithMessage("Project name is required");
        RuleFor(x => x.Images).NotEmpty().WithMessage("Images is required");
    }
}
