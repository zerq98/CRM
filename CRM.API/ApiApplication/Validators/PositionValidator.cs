using ApiApplication.DTO;
using FluentValidation;
using FluentValidation.Validators;

namespace ApiApplication.Validators
{
    public class PositionValidator : AbstractValidator<SellOpportunityPositionDetailsDto>
    {
        public PositionValidator()
        {
            RuleFor(x => x.Product).NotNull().WithMessage("Produkt musi być wybrany.");
            RuleFor(x => x.Quantity).Must(x => x >= 1).WithMessage("Ilość produktu nie może być równa 0.");
        }
    }
}