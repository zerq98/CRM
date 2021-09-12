using ApiApplication.Helpers;
using ApiDomain.Interface;
using ApiDomain.Entity;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ApiInfrastructure;
using Microsoft.EntityFrameworkCore;

namespace ApiApplication.Lead.AddLead
{
    public class UpsertLeadHandler : IRequestHandler<UpsertLeadCommand, IActionResult>
    {
        private readonly ILeadRepository _leadRepository;
        private readonly IUserRepository _userRepository;
        private ICompanyRepository _companyRepository;
        private readonly AppDbContext _context;

        public UpsertLeadHandler(ILeadRepository leadRepository,IUserRepository userRepository,
                              ICompanyRepository companyRepository,AppDbContext appDbContext)
        {
            _leadRepository = leadRepository;
            _userRepository = userRepository;
            _companyRepository = companyRepository;
            _context = appDbContext;
        }
        public async Task<IActionResult> Handle(UpsertLeadCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var lead = await _leadRepository.GetLeadAsync(request.LeadCreateDto.Id,request.CompanyId);

                if (lead == null)
                {
                    lead = new ApiDomain.Entity.Lead();
                    lead.Id = 0;
                    lead.LeadAddress = new LeadAddress();
                    lead.LeadContacts = new List<LeadContact>();
                    lead.Activities = new List<Activity>();
                    lead.CreateDate = DateTime.Now;
                }

                lead.ModificationDate = DateTime.Now;
                lead.Company = await _companyRepository.GetByIdAsync(request.CompanyId);
                lead.LeadStatus = await _context.LeadStatuses.FirstOrDefaultAsync(x => x.Name == request.LeadCreateDto.LeadStatus);
                lead.Name = request.LeadCreateDto.Name;
                lead.NIP = request.LeadCreateDto.NIP;
                lead.Regon = request.LeadCreateDto.Regon;
                lead.User = await _userRepository.GetUserByNameAsync(request.LeadCreateDto.User);

                lead.LeadAddress.ApartmentNumber = request.LeadCreateDto.LeadAddress.ApartmentNumber;
                lead.LeadAddress.City = request.LeadCreateDto.LeadAddress.City;
                lead.LeadAddress.HouseNumber = request.LeadCreateDto.LeadAddress.HouseNumber;
                lead.LeadAddress.PostCode = request.LeadCreateDto.LeadAddress.PostCode;
                lead.LeadAddress.Province = request.LeadCreateDto.LeadAddress.Province;
                lead.LeadAddress.Street = request.LeadCreateDto.LeadAddress.Street;

                foreach (var contact in request.LeadCreateDto.LeadContacts)
                {
                    if (!contact.Deleted)
                    {
                        if (contact.Id > 0)
                        {
                            lead.LeadContacts[lead.LeadContacts.FindIndex(x => x.Id == contact.Id)].Name = contact.Name;
                            lead.LeadContacts[lead.LeadContacts.FindIndex(x => x.Id == contact.Id)].PhoneNumber=contact.PhoneNumber;
                            lead.LeadContacts[lead.LeadContacts.FindIndex(x => x.Id == contact.Id)].Email = contact.Email;
                            lead.LeadContacts[lead.LeadContacts.FindIndex(x => x.Id == contact.Id)].Department = contact.Department;
                        }
                        else
                        {
                            lead.LeadContacts.Add(new LeadContact
                            {
                                Department = contact.Department,
                                Email = contact.Email,
                                PhoneNumber = contact.PhoneNumber,
                                Name = contact.Name,
                                Lead = lead,
                                Id = 0
                            });
                        }
                    }
                    else
                    {
                        if (contact.Id > 0)
                        {
                            await _leadRepository.RemoveLeadContactAsync(contact.Id);
                        }
                    }
                }

                foreach(var activity in request.LeadCreateDto.Activities)
                {
                    if (!activity.Deleted)
                    {
                        if (activity.Id == 0)
                        {
                            lead.Activities.Add(new Activity
                            {
                                ActivityType = await _context.ActivityTypes.FirstOrDefaultAsync(x => x.Name == activity.ActivityType),
                                Lead = lead,
                                User = await _userRepository.GetUserByIdAsync(request.UserId),
                                ActivityDate=DateTime.Now,
                                Id=0
                            });
                        }
                    }
                    else
                    {
                        if (activity.Id > 0)
                        {
                            await _leadRepository.RemoveActivityAsync(activity.Id);
                        }
                    }
                }

                if (lead.Id == 0)
                {
                    lead = await _leadRepository.AddLeadAsync(lead);
                }
                else
                {
                    lead = await _leadRepository.UpdateAsync(lead);
                }

                if (lead != null)
                {
                    return new JsonResult(new ApiResponse<object>
                    {
                        Data = null,
                        Code = 201,
                        ErrorMessage = ""
                    });
                }
                else
                {
                    return new JsonResult(new ApiResponse<object>
                    {
                        Data = null,
                        Code = 400,
                        ErrorMessage = "Coś poszło nie tak, sprawdź dane i spróbuj ponownie."
                    });
                }
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
