using ApiApplication.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiApplication.Validators
{
    public class UserUpsertValidator : AbstractValidator<UserUpsertDto>
    {
        public UserUpsertValidator()
        {
            RuleFor(x => x.Department).NotNull().NotEmpty().WithMessage("Użytkownik musi posiadać jakieś stanowisko.");
            RuleFor(x => x.Email).NotNull().NotEmpty()
                .Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").WithMessage("Email jest nie poprawny.");
            RuleFor(x => x.FirstName).NotNull().NotEmpty().WithMessage("Użytkownik musi mieć imie.");
            RuleFor(x => x.Gender).NotNull().WithMessage("Użytkownik musi mieć płeć.");
            RuleFor(x => x.LastName).NotNull().NotEmpty().WithMessage("Użytkownik musi mieć nazwisko.");
            RuleFor(x => x.Login).NotNull().NotEmpty().WithMessage("Użytkownik musi mieć login.");
            RuleFor(x => x.Password).Matches("^(?=.*[A-Z])(?=.*[!@#$&*])(?=.*[0-9])(?=.*[a-z]).{10,}$")
                .WithMessage("Hasło musi zawierać co najmniej 10 znaków, 1 małą literę, 1 dużą literę, 1 cyfrę, 1 znak specjalny").When(x=> x.Password!=null &&x.Password!="");
            RuleFor(x => x.PhoneNumber).NotNull().NotEmpty().Matches(@"\d{3}-? *\d{3}-? *-?\d{3}").WithMessage("Nie poprawny numer telefonu.");

        }
    }
}
