using FluentValidation;
using GHotel.Application.Models;

namespace GHotel.API.Infrastructure.Validators;

public class ProjectRequestModelValidator : AbstractValidator<ProjectRequestModel>
{
    public ProjectRequestModelValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.CreationDate).NotEmpty().WithMessage("Creation date is required");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
        RuleFor(x => x.StartDate).NotEmpty().WithMessage("Start date is required");
        RuleFor(x => x.EndDate).NotEmpty().WithMessage("End date is required");
        RuleFor(x => x.RequiredBudget).GreaterThan(0).WithMessage("Required budget must be greater than 0");
        RuleFor(x => x.BusinessId).GreaterThan(0).WithMessage("Business id must be greater than 0");
    }
}
