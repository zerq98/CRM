using ApiApplication.DTO;
using FluentValidation;
using FluentValidation.Validators;
using System.Collections.Generic;

namespace ApiApplication.Validators
{
    public class LeadActivitiesValidator : AbstractValidator<ActivityDetailsDto>
    {
        public LeadActivitiesValidator()
        {
            RuleFor(x => x.ActivityType).NotNull().NotEmpty().WithMessage("Aktywność musi być odpowiedniego typu.");
            RuleFor(x => x.User).NotNull().NotEmpty().WithMessage("Aktywność musi mieć przypisaną osobę");
        }
    }
}