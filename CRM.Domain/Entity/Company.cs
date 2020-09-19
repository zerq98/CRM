using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CRM.Domain.Entity
{
    public class Company
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CEOId { get; set; }

        public ApplicationUser CEO { get; set; }

        public int AddressDetailsId { get; set; }

        public CompanyAddressDetails AddressDetails { get; set; }

        public string NIP { get; set; }

        public string FAX { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Description { get; set; }

        public List<ApplicationUser> Employees { get; set; }
    }
}
