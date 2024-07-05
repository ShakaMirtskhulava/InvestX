using FluentValidation;
using GHotel.API.Models;

namespace GHotel.API.Infrastructure.Validators;

public class InvestCreateModelValidator : AbstractValidator<InvestCreateModel>
{
    public InvestCreateModelValidator()
    {
        RuleFor(x => x.ProjectName).NotEmpty().WithMessage("Project name is required");
        RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Amount must be greater than 0");
    }
}
