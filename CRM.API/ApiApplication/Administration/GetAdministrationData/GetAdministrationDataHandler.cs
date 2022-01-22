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

namespace ApiApplication.Administration.GetAdministrationData
{
    public class GetAdministrationDataHandler : IRequestHandler<GetAdministrationDataQuery, IActionResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IOpportunityRepository _opportunityRepository;
        private readonly ILeadRepository _leadRepository;
        private readonly IActivityTypeRepository _activityTypeRepository;
        private readonly ICompanyRepository _companyRepository;

        public GetAdministrationDataHandler(IUserRepository userRepository,IOpportunityRepository opportunityRepository,
                                            ILeadRepository leadRepository,IActivityTypeRepository activityTypeRepository,
                                            ICompanyRepository companyRepository)
        {
            _userRepository = userRepository;
            _opportunityRepository = opportunityRepository;
            _leadRepository = leadRepository;
            _activityTypeRepository = activityTypeRepository;
            _companyRepository = companyRepository;
        }
        public async Task<IActionResult> Handle(GetAdministrationDataQuery request, CancellationToken cancellationToken)
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

                var users = await _userRepository.GetCompanyTraders(request.CompanyId);
                var administrationData = new AdministrationDataDto
                {
                    Users = new List<UserForAdministrationDto>(),
                    Statistics= new CompanyStatisticsDto
                    {
                        Activities=new List<int>(),
                        Opportunities=new List<int>(),
                        ThisMonthGross=0,
                        ThisMonthMarkup=0,
                        ThisMonthNet=0,
                        ThisYearGross=0,
                        ThisYearMarkup=0,
                        ThisYearNet=0
                    },
                };

                var company = await _companyRepository.GetByIdAsync(request.CompanyId);

                administrationData.Company = new CompanyDataDto
                {
                    Address = new AddressDto
                    {
                        ApartmentNumber = company.Address.ApartmentNumber,
                        City = company.Address.City,
                        HouseNumber = company.Address.HouseNumber,
                        Id = company.Address.Id,
                        PostCode = company.Address.PostCode,
                        Province = company.Address.Province,
                        Street = company.Address.Street
                    },
                    Id = company.Id,
                    Name = company.CompanyName,
                    Nip = company.NIP,
                    Regon = company.Regon
                };

                var oppos = await _opportunityRepository.GetAllOpportunitiesAsync(request.CompanyId, new List<string> {"","","" }, new DateTime(DateTime.Now.Year-1, 1, 1), new DateTime(DateTime.Now.Year, 12, 31));
                oppos.AddRange(await _opportunityRepository.GetAllOrdersAsync(request.CompanyId, new List<string> { "", "" }, new DateTime(DateTime.Now.Year-1, 1, 1), new DateTime(DateTime.Now.Year, 12, 31)));

                foreach(var oppo in oppos)
                {
                    administrationData.Statistics.ThisYearGross += oppo.SumGrossValue;
                    administrationData.Statistics.ThisYearMarkup += oppo.SumMarkupValue;
                    administrationData.Statistics.ThisYearNet += oppo.SumNetValue;

                    if (oppo.CreateDate.Month == DateTime.Now.Month)
                    {
                        administrationData.Statistics.ThisMonthGross += oppo.SumGrossValue;
                        administrationData.Statistics.ThisMonthMarkup += oppo.SumMarkupValue;
                        administrationData.Statistics.ThisMonthNet += oppo.SumNetValue;
                    }
                }

                var oppoStatuses = new List<string>
                {
                    "Nowa",
                    "Modyfikowana",
                    "Anulowana",
                    "Zaakceptowana",
                    "Oferta"
                };

                foreach(var oppoStat in oppoStatuses)
                {
                    administrationData.Statistics.Opportunities.Add(oppos.Where(x => x.Status.Name == oppoStat).Count());
                }

                foreach (var user in users)
                {
                    administrationData.Users.Add(new UserForAdministrationDto
                    {
                        Id = user.Id,
                        CanDelete = (user.Id != request.UserId),
                        Department = user.CompanyPosition,
                        Name = user.FirstName + " " + user.LastName,
                        StartDate = user.WorkStartDate.ToString("dd.MM.yyyy"),
                        Gender = user.Gender
                    });
                }

                var activities = await _activityTypeRepository.GetActivityTypesAsync();
                var leads = await _leadRepository.GetAllLeadsAsync(request.CompanyId, new DateTime(DateTime.Now.Year, 1, 1), new DateTime(DateTime.Now.Year, 12, 31));

                foreach(var activity in activities)
                {
                    administrationData.Statistics.Activities.Add(0);
                }

                foreach(var lead in leads)
                {
                    int counter = 0;
                    foreach (var activity in activities)
                    {
                        administrationData.Statistics.Activities[counter] += lead.Activities.Where(x => x.ActivityType == activity).Count();
                        counter += 1;
                    }
                }

                return new JsonResult(new ApiResponse<AdministrationDataDto>
                {
                    Code = 200,
                    Data = administrationData,
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
