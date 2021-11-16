using ApiApplication.DTO;
using FluentValidation;
using FluentValidation.Validators;

namespace ApiApplication.Validators
{
    public class LeadAddressValidator : AbstractValidator<LeadAddressDto>
    {
        public LeadAddressValidator()
        {
            RuleFor(x => x.Street).NotNull().NotEmpty().WithMessage("Nazwa ulicy jest wymagana.");
            RuleFor(x => x.Province).NotNull().NotEmpty().WithMessage("Województwo jest wymagane.");
            RuleFor(x => x.PostCode).NotNull().NotEmpty().Matches("^[0-9]{2}-[0-9]{3}$").WithMessage("Nie poprawny kod pocztowy.");
            RuleFor(x => x.HouseNumber).NotNull().NotEmpty().WithMessage("Numer domu jest wymagany.");
            RuleFor(x => x.City).NotNull().NotEmpty().WithMessage("Miasto jest wymagane.");
        }
    }
}