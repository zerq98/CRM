using ApiApplication.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiApplication.Validators
{
    public class SellOpportunityDetailsValidator : AbstractValidator<SellOpportunityDetailsDto>
    {
        public SellOpportunityDetailsValidator()
        {
            RuleFor(x => x.Lead).NotNull().NotEmpty().WithMessage("Szansa sprzedaży musi dotyczyć jakiegoś leada.");
            RuleFor(x => x.Status).NotNull().NotEmpty().WithMessage("Szansa sprzedaży musi posiadać jakiś status.");
            RuleFor(x => x.Trader).NotNull().NotEmpty().WithMessage("Szansę sprzedaży musi obsługiwać jakiś handlowiec.");
            RuleFor(x => x.Positions).NotNull().Must(x => x.Count >= 1).WithMessage("Szansa sprzedaży musi posiadać przynajmniej jedną pozycję.");
            RuleForEach(x => x.Positions).SetValidator(new PositionValidator());
        }
    }
}
