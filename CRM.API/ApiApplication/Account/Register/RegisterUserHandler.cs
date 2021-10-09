using ApiApplication.Helpers;
using ApiApplication.Helpers.Email;
using ApiDomain.Entity;
using ApiDomain.Interface;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApiApplication.Account.Register
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, IActionResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IBaseRepository _baseRepository;
        private readonly IClaimRepository _claimRepository;

        public RegisterUserHandler(IUserRepository userRepository, ICompanyRepository companyRepository,
                                   IAddressRepository addressRepository, IClaimRepository claimRepository,
                                   IBaseRepository baseRepository)
        {
            _userRepository = userRepository;
            _addressRepository = addressRepository;
            _companyRepository = companyRepository;
            _baseRepository = baseRepository;
            _claimRepository = claimRepository;
        }

        public async Task<IActionResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            using (var transaction = _baseRepository.GetTransaction())
            {
                try
                {
                    if (await _userRepository.GetUserByEmailAsync(request.Data.User.Email) != null)
                    {
                        return new JsonResult(new ApiResponse<object>
                        {
                            Code = 409,
                            ErrorMessage = "Ten adres email jest już w użyciu.",
                            Data = null
                        });
                    }

                    if (await _userRepository.GetUserByLoginAsync(request.Data.User.Login) != null)
                    {
                        return new JsonResult(new ApiResponse<object>
                        {
                            Code = 409,
                            ErrorMessage = "Ta nazwa użytkownika jest już w użyciu.",
                            Data = null
                        });
                    }

                    if(await _companyRepository.GetByNameAsync(request.Data.Company.CompanyName) != null)
                    {
                        return new JsonResult(new ApiResponse<object>
                        {
                            Code = 409,
                            ErrorMessage = "Ta firma istnieje w bazie danych.",
                            Data = null
                        });
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
                        NIP = "",
                        Employees = new List<ApplicationUser>(),
                        Regon = ""
                    };
                    var createdCompany = await _companyRepository.CreateCompanyAsync(company);

                    var user = new ApplicationUser
                    {
                        Address = createdUserAddress,
                        CompanyPosition="IT Team Leader",
                        Email = request.Data.User.Email,
                        EmailConfirmed = false,
                        FirstName = request.Data.User.FirstName,
                        LastName = request.Data.User.LastName,
                        NormalizedEmail = request.Data.User.Email,
                        NormalizedUserName = request.Data.User.Login,
                        UserName = request.Data.User.Login,
                        PhoneNumber = request.Data.User.PhoneNumber,
                        Company=createdCompany,
                        Gender=request.Data.User.Gender,
                        WorkStartDate=DateTime.Now.Date
                    };
                    var createdUser = await _userRepository.CreateUserAsync(user, request.Data.User.Password);
                    var appClaims = await _claimRepository.GetApplicationClaimsAsync();

                    await _userRepository.AssignClaimsAsync(appClaims, createdUser.Id);

                    var token = await _userRepository.GenerateEmailConfirmationTokenAsync(createdUser);
                    var confirmationLink = $"http://localhost:3000/ConfirmEmail?userId={createdUser.Id}&token={token}";

                    EmailSender sender = new EmailSender();
                    await sender.SendEmailAsync(createdUser.Email, "Konto zostało utworzone",
                        $"Twój link do potwierdzenia konta:<br>{confirmationLink}<br>Dziękujemy za skorzystanie z naszych usług");

                    transaction.Commit();
                    return new JsonResult(new ApiResponse<object>
                    {
                        Code = 201,
                        Data = null,
                        ErrorMessage = ""
                    });
                }
                catch
                {
                    transaction.Rollback();
                    return new JsonResult(new ApiResponse<object>
                    {
                        Code = 500,
                        Data = null,
                        ErrorMessage = "Nastąpił problem z serwerem, skontaktuj się z działem IT."
                    });
                }
            }
        }
    }
}