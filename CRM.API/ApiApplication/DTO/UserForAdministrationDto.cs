using System;

namespace ApiApplication.DTO
{
    public class UserForAdministrationDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string StartDate { get; set; }
        public bool CanDelete { get; set; }
        public bool Gender { get; set; }
    }
}