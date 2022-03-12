using CleaningManagement.BLL.CleaningPlans.Models;
using FluentValidation;

namespace CleaningManagement.BLL.CleaningPlans.Validators
{
    public class UpdateCleaningPlanRequestValidator : AbstractValidator<UpdateCleaningPlanRequest>
    {
        public UpdateCleaningPlanRequestValidator()
        {
            RuleFor(r => r.Title).NotEmpty().WithMessage("Title is mandatory.");
            RuleFor(r => r.Title).MaximumLength(256).WithMessage("Title cannot be longer than 256 characters.");
            RuleFor(r => r.CustomerId).GreaterThan(0).WithMessage("Customer id must be a positive integer.");
            RuleFor(r => r.Description).MaximumLength(512).WithMessage("Description cannot be longer than 512 characters.");
        }
    }
}
