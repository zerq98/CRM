using ApiApplication.Account.Register;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiApplication.Validators
{
    public class RegisterDataValidator : AbstractValidator<RegisterData>
    {
        public RegisterDataValidator()
        {
            RuleFor(x => x.Company).SetValidator(new CreateCompanyValidator());
            RuleFor(x => x.CompanyAddress).SetValidator(new CreateAddressValidator());
            RuleFor(x => x.User).SetValidator(new RegisterUserValidator());
        }
    }
}
