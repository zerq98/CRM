using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Application.Dto.User
{
    public class ApplicationUserVM
    {
        public ApplicationUserVM()
        {
            Roles = new List<string>();
        }

        public string Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PrivateEmail { get; set; }

        public string PhoneNumber { get; set; }

        public List<string> Roles { get; set; }
    }
}
