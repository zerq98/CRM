using ApiApplication.Helpers.Email;
using ApiDomain.Entity;
using ApiDomain.Interface;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApiApplication.Account.Register
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, IActionResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public RegisterUserHandler(IUserRepository userRepository,ICompanyRepository companyRepository,
                                   IAddressRepository addressRepository, IDepartmentRepository departmentRepository)
        {
            _userRepository = userRepository;
            _addressRepository = addressRepository;
            _companyRepository = companyRepository;
            _departmentRepository = departmentRepository;
        }

        public async Task<IActionResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if(await _userRepository.GetUserByEmailAsync(request.Data.User.Email)!=null)
                {
                    return new JsonResult("Email exists");
                }

                if (await _userRepository.GetUserByLoginAsync(request.Data.User.Login) != null)
                {
                    return new JsonResult("Login exists");
                }

                var userAddress = new Address
                {
                    ApartmentNumber = request.Data.UserAddress.ApartmentNumber,
                    City = request.Data.UserAddress.City,
                    HouseNumber = request.Data.UserAddress.HouseNumber,
                    PostCode = request.Data.UserAddress.PostCode,
                    Province = request.Data.UserAddress.Province,
                    Street = request.Data.UserAddress.Street
                };
                var createdUserAddress = await _addressRepository.CreateAddressAsync(userAddress);

                var companyAddress = new Address
                {
                    ApartmentNumber = request.Data.CompanyAddress.ApartmentNumber,
                    City = request.Data.CompanyAddress.City,
                    HouseNumber = request.Data.CompanyAddress.HouseNumber,
                    PostCode = request.Data.CompanyAddress.PostCode,
                    Province = request.Data.CompanyAddress.Province,
                    Street = request.Data.CompanyAddress.Street
                };
                var createdCompanyAddress = await _addressRepository.CreateAddressAsync(companyAddress);

                var company = new Company
                {
                    Address = createdCompanyAddress,
                    CompanyName = request.Data.Company.CompanyName,
                    NIP = request.Data.Company.NIP,
                    Departments = new List<Department>(),
                    Regon = request.Data.Company.Regon
                };
                var createdCompany = await _companyRepository.CreateCompanyAsync(company);

                var department = new Department
                {
                    Company = createdCompany,
                    Name = request.Data.Department.Name,
                    Users = new List<ApplicationUser>()
                };
                var createdDepartment = await _departmentRepository.CreateDepartmentAsync(department);

                var user = new ApplicationUser
                {
                    Address = createdUserAddress,
                    Department = createdDepartment,
                    Email = request.Data.User.Email,
                    EmailConfirmed = false,
                    FirstName = request.Data.User.FirstName,
                    LastName = request.Data.User.LastName,
                    NormalizedEmail = request.Data.User.Email,
                    NormalizedUserName = request.Data.User.Login,
                    UserName = request.Data.User.Login,
                    PhoneNumber = request.Data.User.PhoneNumber
                };
                var createdUser = await _userRepository.CreateUserAsync(user, request.Data.User.Password);

                var token = await _userRepository.GenerateEmailConfirmationTokenAsync(createdUser);
                var confirmationLink = $"http://localhost:44395/Account/ConfirmEmail?userId={createdUser.Id}&token={token}";

                EmailSender sender = new EmailSender();
                await sender.SendEmailAsync(createdUser.Email, "Konto zostało utworzone",
                    $"Twój link do potwierdzenia konta:<br>{confirmationLink}<br>Dziękujemy za skorzystanie z naszych usług");

                return new JsonResult(createdUser);
            }
            catch
            {
                return new StatusCodeResult(500);
            }
        }
    }
}
