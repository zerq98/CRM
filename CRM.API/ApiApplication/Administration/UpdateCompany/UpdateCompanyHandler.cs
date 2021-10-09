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

namespace ApiApplication.Administration.UpdateCompany
{
    public class UpdateCompanyHandler : IRequestHandler<UpdateCompanyCommand, IActionResult>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IUserRepository _userRepository;

        public UpdateCompanyHandler(IUserRepository userRepository,ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (!(await PermissionMonitor.CheckPermissionsAsync(_userRepository, request.UserId, "Panel administracji")))
                {
                    return new JsonResult(new ApiResponse<object>
                    {
                        Data = null,
                        Code = 403,
                        ErrorMessage = "Brak uprawnień"
                    });
                }

                var company = new Company
                {
                    Address = new Address
                    {
                        ApartmentNumber = request.Dto.Address.ApartmentNumber,
                        City = request.Dto.Address.City,
                        HouseNumber = request.Dto.Address.HouseNumber,
                        Id = request.Dto.Address.Id,
                        PostCode = request.Dto.Address.PostCode,
                        Province = request.Dto.Address.Province,
                        Street = request.Dto.Address.Street
                    },
                    Id = request.Dto.Id,
                    CompanyName = request.Dto.Name,
                    NIP = request.Dto.Nip,
                    Regon = request.Dto.Regon
                };

                await _companyRepository.UpdateCompanyAsync(company);

                return new JsonResult(new ApiResponse<object>
                {
                    Code = 201,
                    Data = null,
                    ErrorMessage = ""
                });
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
