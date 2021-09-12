using System.Collections.Generic;

namespace ApiDomain.Entity
{
    public class Company
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string NIP { get; set; }
        public string Regon { get; set; }
        public Address Address { get; set; }
        public int AddressId { get; set; }
        public List<ApplicationUser> Employees { get; set; }
        public List<Lead> Leads { get; set; }
    }
}