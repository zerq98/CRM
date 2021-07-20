using ApiApplication.DTO;

namespace ApiApplication.Account.Register
{
    public class RegisterData
    {
        public RegisterUserDto User { get; set; }
        public CreateAddressDto CompanyAddress { get; set; }
        public CreateAddressDto UserAddress { get; set; }
        public CreateCompanyDto Company { get; set; }
    }
}