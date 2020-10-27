using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Application.Dto.User
{
    public class ApplicationUserCreateVM
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string PrivateEmail { get; set; }
    }
}
