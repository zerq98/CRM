using ApiApplication.DTO;
using ApiApplication.Helpers;
using ApiDomain.Interface;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApiApplication.Administration.GetUserData
{
    public class GetUserDataHandler : IRequestHandler<GetUserDataQuery, IActionResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IBaseRepository _baseRepository;

        public GetUserDataHandler(IUserRepository userRepository, IBaseRepository baseRepository)
        {
            _userRepository = userRepository;
            _baseRepository = baseRepository;
        }

        public async Task<IActionResult> Handle(GetUserDataQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (!(await PermissionMonitor.CheckPermissionsAsync(_userRepository, request.RequestUserId, "Panel administracji")))
                {
                    return new JsonResult(new ApiResponse<object>
                    {
                        Data = null,
                        Code = 403,
                        ErrorMessage = "Brak uprawnień"
                    });
                }

                var user = await _userRepository.GetUserByIdAsync(request.UserId);
                if (user != null)
                {
                    var userData = new UserUpsertDto
                    {
                        Id = user.Id,
                        Address = new AddressDto
                        {
                            Id = user.Address.Id,
                            ApartmentNumber = user.Address.ApartmentNumber,
                            City = user.Address.City,
                            HouseNumber = user.Address.HouseNumber,
                            PostCode = user.Address.PostCode,
                            Province = user.Address.Province,
                            Street = user.Address.Street
                        },
                        Department=user.CompanyPosition,
                        Email=user.Email,
                        FirstName=user.FirstName,
                        Gender=user.Gender,
                        LastName=user.LastName,
                        Login=user.UserName,
                        Password="",
                        Permissions=new List<PermissionDto>(),
                        PhoneNumber=user.PhoneNumber
                    };

                    var allPerm = await _userRepository.GetAppClaims();
                    var userPerms = await _userRepository.GetUserClaims(user.Id);

                    foreach(var claim in allPerm)
                    {
                        userData.Permissions.Add(new PermissionDto
                        {
                            Name = claim,
                            Selected = userPerms.Contains(claim)
                        });
                    }

                    return new JsonResult(new ApiResponse<UserUpsertDto>
                    {
                        Code = 200,
                        Data = userData,
                        ErrorMessage = ""
                    });
                }
                else
                {
                    var userData = new UserUpsertDto
                    {
                        Id = "",
                        Address = new AddressDto
                        {
                            Id = 0,
                            ApartmentNumber = "",
                            City = "",
                            HouseNumber = "",
                            PostCode = "",
                            Province = "",
                            Street = ""
                        },
                        Department = "",
                        Email = "",
                        FirstName = "",
                        Gender = false,
                        LastName = "",
                        Login = "",
                        Password = "",
                        Permissions = new List<PermissionDto>(),
                        PhoneNumber = ""
                    };

                    var allPerm = await _userRepository.GetAppClaims();

                    foreach (var claim in allPerm)
                    {
                        userData.Permissions.Add(new PermissionDto
                        {
                            Name = claim,
                            Selected = false
                        });
                    }

                    return new JsonResult(new ApiResponse<UserUpsertDto>
                    {
                        Code = 200,
                        Data = userData,
                        ErrorMessage = ""
                    });
                }
            }
            catch
            {
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
