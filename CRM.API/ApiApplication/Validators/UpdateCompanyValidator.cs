using ApiApplication.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiApplication.Validators
{
    public class UpdateCompanyValidator : AbstractValidator<CompanyDataDto>
    {
        public UpdateCompanyValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Firma musi posiadać nazwę.");
            RuleFor(x => x.Address).SetValidator(new AddressValidator());
        }
    }
}
