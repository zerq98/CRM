using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace CRM.Domain.Entity
{
    public class ApplicationUser : IdentityUser
    {
        public List<Customer> Customers { get; set; }

        public bool IsActive { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PrivateEmail { get; set; }
    }
}