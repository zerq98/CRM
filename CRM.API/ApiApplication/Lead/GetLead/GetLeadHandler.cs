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

namespace ApiApplication.Lead.GetLead
{
    public class GetLeadHandler : IRequestHandler<GetLeadQuery, IActionResult>
    {
        private readonly ILeadRepository _leadRepository;
        private readonly IUserRepository _userRepository;

        public GetLeadHandler(ILeadRepository leadRepository, IUserRepository userRepository)
        {
            _leadRepository = leadRepository;
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Handle(GetLeadQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var traders = await _userRepository.GetCompanyTraders(request.CompanyId);
                var responseTraders = new List<string>();

                foreach(var trader in traders)
                {
                    responseTraders.Add(trader.FirstName + " " + trader.LastName);
                }

                if (request.Id == 0)
                {
                    return new JsonResult(new ApiResponse<LeadDetailsDto>
                    {
                        Code = 200,
                        ErrorMessage = "",
                        Data = new LeadDetailsDto
                        {
                            Lead = new LeadForDetailsDto
                            {
                                Id = 0,
                                Activities = new List<ActivityDetailsDto>(),
                                LeadAddress = new LeadAddressDto
                                {
                                    Id = 0,
                                    ApartmentNumber = "",
                                    City = "",
                                    HouseNumber = "",
                                    PostCode = "",
                                    Province = "",
                                    Street = ""

                                },
                                LeadContacts = new List<LeadContactDto>(),
                                LeadStatus = "",
                                Name = "",
                                NIP = "",
                                Regon = "",
                                User = ""
                            },
                            CompanyTraders = responseTraders
                        }
                    });
                }

                var lead = await _leadRepository.GetLeadAsync(request.Id,request.CompanyId);

                if (lead != null)
                {
                    var leadResponse = new LeadForDetailsDto
                    {
                        Id = lead.Id,
                        Activities = new List<ActivityDetailsDto>(),
                        LeadContacts = new List<LeadContactDto>(),
                        LeadStatus = lead.LeadStatus.Name,
                        Name = lead.Name,
                        NIP = lead.NIP,
                        Regon = lead.Regon,
                        User = lead.User.FirstName + " " + lead.User.LastName
                    };

                    leadResponse.LeadAddress = new LeadAddressDto
                    {
                        Id = lead.LeadAddress.Id,
                        ApartmentNumber = lead.LeadAddress.ApartmentNumber,
                        City = lead.LeadAddress.City,
                        HouseNumber = lead.LeadAddress.HouseNumber,
                        PostCode = lead.LeadAddress.PostCode,
                        Province = lead.LeadAddress.Province,
                        Street = lead.LeadAddress.Street
                    };

                    var counter = 1;
                    foreach(var contact in lead.LeadContacts)
                    {
                        leadResponse.LeadContacts.Add(new LeadContactDto
                        {
                            Deleted = false,
                            Department = contact.Department,
                            Email = contact.Email,
                            Id = contact.Id,
                            LocalId = counter,
                            Name = contact.Name,
                            PhoneNumber = contact.PhoneNumber
                        });
                        counter++;
                    }

                    counter = 1;
                    foreach(var activity in lead.Activities)
                    {
                        leadResponse.Activities.Add(new ActivityDetailsDto
                        {
                            Deleted = false,
                            ActivityDate = activity.ActivityDate,
                            ActivityType = activity.ActivityType.Name,
                            LocalId = counter,
                            Id = activity.Id,
                            User = activity.User.FirstName + " " + activity.User.LastName
                        });
                        counter++;
                    }

                    return new JsonResult(new ApiResponse<LeadDetailsDto>
                    {
                        Code = 200,
                        ErrorMessage = "",
                        Data = new LeadDetailsDto
                        {
                            Lead = leadResponse,
                            CompanyTraders = responseTraders
                        }
                    });
                }

                return new JsonResult(new ApiResponse<object>
                {
                    Data = null,
                    Code = 404,
                    ErrorMessage = "Nie znaleziono leada o id="+request.Id+", skontaktuj się z działem IT."
                });
            }
            catch
            {
                return new JsonResult(new ApiResponse<object>
                {
                    Data = null,
                    Code = 500,
                    ErrorMessage = "Nastąpił problem z serwerem, skontaktuj się z działem IT."
                });
            }
        }
    }
}
