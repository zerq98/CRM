using System.Collections.Generic;

namespace ApiDomain.Entity
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Company Company { get; set; }
        public int CompanyId { get; set; }
        public List<ApplicationUser> Users { get; set; }
    }
}