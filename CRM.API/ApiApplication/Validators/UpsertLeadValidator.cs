using ApiApplication.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiApplication.Validators
{
    public class UpsertLeadValidator : AbstractValidator<LeadForDetailsDto>
    {
        public UpsertLeadValidator()
        {
            RuleForEach(x => x.Activities).SetValidator(new LeadActivitiesValidator()).When(x => x.Activities.Count > 0);
            RuleFor(x => x.LeadAddress).SetValidator(new LeadAddressValidator());
            RuleForEach(x => x.LeadContacts).SetValidator(new LeadContactsValidator()).When(x => x.LeadContacts.Count > 0);
            RuleFor(x => x.LeadStatus).NotNull().NotEmpty().WithMessage("Lead musi posiadać jakiś status.");
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Lead musi posiadać nazwę.");
            RuleFor(x => x.NIP).NotNull().NotEmpty().Matches("[0-9]{10}").WithMessage("Nie poprawny numer NIP.");
            RuleFor(x=>x.Regon).NotNull().NotEmpty().Matches("[0-9]{7,9}").WithMessage("Nie poprawny numer Regon.");
            RuleFor(x=>x.User).NotNull().NotEmpty().WithMessage("Lead musi mieć przypisanego handlowca.");
        }
    }
}
