﻿using Microsoft.AspNetCore.Identity;

namespace ApiDomain.Entity
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }
        public int AddressId { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
    }
}