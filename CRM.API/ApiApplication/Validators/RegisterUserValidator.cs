using ApiApplication.Account.Register;
using ApiApplication.DTO;
using FluentValidation;
using FluentValidation.Validators;

namespace ApiApplication.Validators
{
    public class RegisterUserValidator : AbstractValidator< RegisterUserDto>
    {
        public RegisterUserValidator()
        {
            RuleFor(x => x.Email).NotNull().NotEmpty()
                .Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            RuleFor(x => x.FirstName).NotNull().NotEmpty().WithMessage("Użytkownik musi mieć imie.");
            RuleFor(x => x.Gender).NotNull().WithMessage("Użytkownik musi mieć płeć.");
            RuleFor(x => x.LastName).NotNull().NotEmpty().WithMessage("Użytkownik musi mieć nazwisko.");
            RuleFor(x => x.Login).NotNull().NotEmpty().WithMessage("Użytkownik musi mieć login.");
            RuleFor(x => x.Password).NotNull().NotEmpty().Matches("^(?=.*[A-Z])(?=.*[!@#$&*])(?=.*[0-9])(?=.*[a-z]).{10,}$")
                .WithMessage("Hasło musi zawierać co najmniej 10 znaków, 1 małą literę, 1 dużą literę, 1 cyfrę, 1 znak specjalny");
            RuleFor(x => x.PhoneNumber).NotNull().NotEmpty().Matches(@"\d{3}-? *\d{3}-? *-?\d{3}").WithMessage("Nie poprawny numer telefonu.");

        }
    }
}