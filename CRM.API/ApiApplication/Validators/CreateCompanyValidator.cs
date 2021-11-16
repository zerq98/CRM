using ApiApplication.Account.Register;
using ApiApplication.DTO;
using FluentValidation;
using FluentValidation.Validators;

namespace ApiApplication.Validators
{
    public class CreateCompanyValidator : AbstractValidator< CreateCompanyDto>
    {
        public CreateCompanyValidator()
        {
            RuleFor(x => x.CompanyName).NotNull().NotEmpty().WithMessage("Firma musi posiadać nazwę.");
        }
    }
}