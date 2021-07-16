using ApiApplication.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiApplication.Account.Register
{
    public class RegisterData
    {
        public RegisterUserDto User { get; set; }
        public CreateAddressDto CompanyAddress { get; set; }
        public CreateAddressDto UserAddress { get; set; }
        public CreateCompanyDto Company { get; set; }
        public CreateDepartmentDto Department { get; set; }
    }
}
