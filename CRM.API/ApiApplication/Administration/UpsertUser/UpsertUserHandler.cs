using ApiApplication.Helpers;
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

namespace ApiApplication.Administration.UpsertUser
{
    public class UpsertUserHandler : IRequestHandler<UpsertUserCommand, IActionResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IBaseRepository _baseRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IClaimRepository _claimRepository;

        public UpsertUserHandler(IUserRepository userRepository,IBaseRepository baseRepository,
                                 IAddressRepository addressRepository,ICompanyRepository companyRepository,
                                 IClaimRepository claimRepository)
        {
            _userRepository = userRepository;
            _baseRepository = baseRepository;
            _addressRepository = addressRepository;
            _companyRepository = companyRepository;
            _claimRepository = claimRepository;
        }
        public async Task<IActionResult> Handle(UpsertUserCommand request, CancellationToken cancellationToken)
        {
            using (var transaction = _baseRepository.GetTransaction())
            {
                try
                {
                    if (request.Dto.Id=="" && await _userRepository.GetUserByEmailAsync(request.Dto.Email) != null)
                    {
                        return new JsonResult(new ApiResponse<object>
                        {
                            Code = 409,
                            ErrorMessage = "Ten adres email jest już w użyciu.",
                            Data = null
                        });
                    }

                    if (request.Dto.Id == "" && await _userRepository.GetUserByLoginAsync(request.Dto.Login) != null)
                    {
                        return new JsonResult(new ApiResponse<object>
                        {
                            Code = 409,
                            ErrorMessage = "Ta nazwa użytkownika jest już w użyciu.",
                            Data = null
                        });
                    }

                    var user = new ApplicationUser();

                    if (request.Dto.Id == "")
                    {
                        var userAddress = new Address
                        {
                            ApartmentNumber = request.Dto.Address.ApartmentNumber,
                            City = request.Dto.Address.City,
                            HouseNumber = request.Dto.Address.HouseNumber,
                            PostCode = request.Dto.Address.PostCode,
                            Province = request.Dto.Address.Province,
                            Street = request.Dto.Address.Street,
                            Id = request.Dto.Address.Id
                        };
                        var DBUserAddress = await _addressRepository.CreateAddressAsync(userAddress);

                        user = new ApplicationUser
                        {
                            Address = DBUserAddress,
                            CompanyPosition = "IT Team Leader",
                            Email = request.Dto.Email,
                            EmailConfirmed = false,
                            FirstName = request.Dto.FirstName,
                            LastName = request.Dto.LastName,
                            NormalizedEmail = request.Dto.Email,
                            NormalizedUserName = request.Dto.Login,
                            UserName = request.Dto.Login,
                            PhoneNumber = request.Dto.PhoneNumber,
                            Company = await _companyRepository.GetByIdAsync(request.CompanyId),
                            Gender = request.Dto.Gender,
                            WorkStartDate = DateTime.Now.Date,
                            Id = request.Dto.Id
                        };
                        user = await _userRepository.CreateUserAsync(user, request.Dto.Password);
                    }
                    else
                    {
                        var userAddress = new Address
                        {
                            ApartmentNumber = request.Dto.Address.ApartmentNumber,
                            City = request.Dto.Address.City,
                            HouseNumber = request.Dto.Address.HouseNumber,
                            PostCode = request.Dto.Address.PostCode,
                            Province = request.Dto.Address.Province,
                            Street = request.Dto.Address.Street,
                            Id = request.Dto.Address.Id
                        };
                        var DBUserAddress = new Address();

                        if (userAddress.Id > 0)
                        {
                            DBUserAddress = await _addressRepository.UpdateAddressAsync(userAddress);
                        }
                        else
                        {
                            DBUserAddress = await _addressRepository.CreateAddressAsync(userAddress);
                        }

                        user = new ApplicationUser
                        {
                            Address = DBUserAddress,
                            CompanyPosition = "IT Team Leader",
                            Email = request.Dto.Email,
                            EmailConfirmed = false,
                            FirstName = request.Dto.FirstName,
                            LastName = request.Dto.LastName,
                            NormalizedEmail = request.Dto.Email,
                            NormalizedUserName = request.Dto.Login,
                            UserName = request.Dto.Login,
                            PhoneNumber = request.Dto.PhoneNumber,
                            Company = await _companyRepository.GetByIdAsync(request.CompanyId),
                            Gender = request.Dto.Gender,
                            WorkStartDate = DateTime.Now.Date,
                            Id = request.Dto.Id
                        };
                        user = await _userRepository.UpdateUserAsync(user, request.Dto.Password);
                    }

                    var appClaims = await _claimRepository.GetApplicationClaimsAsync();

                    await _userRepository.AssignClaimsAsync(appClaims, user.Id);

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
