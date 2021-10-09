using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace ApiDomain.Entity
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyPosition { get; set; }
        public DateTime WorkStartDate { get; set; }
        public bool Gender { get; set; }
        public Address Address { get; set; }
        public int AddressId { get; set; }
        public Company Company { get; set; }
        public int CompanyId { get; set; }
        public List<TodoTask> TodoTasks { get; set; }
        public List<Lead> Leads { get; set; }
        public List<Activity> Activities { get; set; }
    }
}