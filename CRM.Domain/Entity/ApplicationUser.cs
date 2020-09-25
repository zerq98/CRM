using Microsoft.AspNetCore.Identity;

namespace CRM.Domain.Entity
{
    public class ApplicationUser : IdentityUser
    {
        public int CompanyId { get; set; }

        public Customer Company { get; set; }

        public int CompanyRoleId { get; set; }

        public CompanyRole CompanyRole { get; set; }
    }
}
