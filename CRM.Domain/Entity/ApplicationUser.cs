using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace CRM.Domain.Entity
{
    public class ApplicationUser : IdentityUser
    {
        public List<Customer> Customers { get; set; }
    }
}