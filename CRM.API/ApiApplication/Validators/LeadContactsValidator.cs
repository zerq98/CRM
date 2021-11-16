using ApiApplication.DTO;
using FluentValidation;
using FluentValidation.Validators;
using System.Collections.Generic;

namespace ApiApplication.Validators
{
    public class LeadContactsValidator : AbstractValidator<LeadContactDto>
    {
        public LeadContactsValidator()
        {
            RuleFor(x => x.Department).NotNull().NotEmpty().WithMessage("Musisz podać stanowisko osoby kontaktowej.");
            RuleFor(x => x.Email).NotNull().NotEmpty().Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").WithMessage("Nie poprawny adres email osoby kontaktowej.");
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Musisz podać imie i nazwisko osoby kontaktowej.");
            RuleFor(x => x.PhoneNumber).NotNull().NotEmpty().Matches(@"\d{3}-? *\d{3}-? *-?\d{3}").WithMessage("Nie poprawny numer telefonu osoby kontaktowej.");
        }
    }
}