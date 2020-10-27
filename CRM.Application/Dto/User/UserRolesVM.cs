using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Application.Dto.User
{
    public class UserRolesVM
    {
        public string RoleId { get; set; }

        public string RoleName { get; set; }

        public bool IsSelected { get; set; }
    }
}
