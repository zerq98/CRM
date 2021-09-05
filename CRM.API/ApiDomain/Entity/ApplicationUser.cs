using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ApiDomain.Entity
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyPosition { get; set; }
        public Address Address { get; set; }
        public int AddressId { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
        public List<TodoTask> TodoTasks { get; set; }
        public List<Lead> Leads { get; set; }
        public List<Activity> Activities { get; set; }
    }
}