using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Desktop.Data.Models
{
    public class UserData
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Department { get; set; }
        public string PhoneNumber { get; set; }
        public bool Gender { get; set; }
        public AddressDto Address { get; set; }
        public List<PermissionDto> Permissions { get; set; }
    }
}
