using CRM.Application.Dto.User;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CRM.Application.Validators
{
    public class UserValidation : AbstractValidator<ApplicationUserCreateVM>
    {
        public UserValidation()
        {
            RuleFor(x => x.Email).NotNull().NotEmpty()
                .Matches(@"^[a-zA-Z0-9._-]+@[a-z0-9]+\.[a-z]{3}$").WithMessage("Email is not valid")
                .Matches(@"^\w+@arctech\.com$").WithMessage("Email is not in work domain");
            RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage("Password cannot be empty");
            RuleFor(x => x.ConfirmPassword).NotNull().NotEmpty().Equal(x => x.Password).WithMessage("Passwords are not equal");
            RuleFor(x => x.FirstName).NotNull().NotEmpty().WithMessage("First name cannot be empty");
            RuleFor(x => x.LastName).NotNull().NotEmpty().WithMessage("Last name cannot be empty");
            RuleFor(x => x.PhoneNumber).NotNull().Length(9).WithMessage("Phone number cannot be empty");
            RuleFor(x => x.PrivateEmail).NotNull().NotEmpty().Matches(@"^[a-zA-Z0-9._-]+@[a-z0-9]+\.[a-z]+$");
        }
    }
}
