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
    public class AddLeadHandler : IRequestHandler<AddLeadCommand, IActionResult>
    {
        private readonly ILeadRepository _leadRepository;
        private readonly IUserRepository _userRepository;
        private ICompanyRepository _companyRepository;
        private readonly AppDbContext _context;

        public AddLeadHandler(ILeadRepository leadRepository,IUserRepository userRepository,
                              ICompanyRepository companyRepository,AppDbContext appDbContext)
        {
            _leadRepository = leadRepository;
            _userRepository = userRepository;
            _companyRepository = companyRepository;
            _context = appDbContext;
        }
        public async Task<IActionResult> Handle(AddLeadCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var lead = new ApiDomain.Entity.Lead
                {
                    Activities = new List<Activity>(),
                    Company = await _companyRepository.GetByIdAsync(request.CompanyId),
                    LeadAddress = new LeadAddress(),
                    LeadContacts = new List<LeadContact>(),
                    LeadStatus = await _context.LeadStatuses.FirstOrDefaultAsync(x => x.Name == request.LeadCreateDto.LeadStatus),
                    Name = request.LeadCreateDto.Name,
                    NIP = request.LeadCreateDto.NIP,
                    Regon = request.LeadCreateDto.Regon,
                    User = await _userRepository.GetUserByIdAsync(request.UserId)
                };

                lead.LeadAddress.ApartmentNumber = request.LeadCreateDto.LeadAddress.ApartmentNumber;
                lead.LeadAddress.City = request.LeadCreateDto.LeadAddress.City;
                lead.LeadAddress.HouseNumber = request.LeadCreateDto.LeadAddress.HouseNumber;
                lead.LeadAddress.PostCode = request.LeadCreateDto.LeadAddress.PostCode;
                lead.LeadAddress.Province = request.LeadCreateDto.LeadAddress.Province;
                lead.LeadAddress.Street = request.LeadCreateDto.LeadAddress.Street;

                foreach (var contact in request.LeadCreateDto.LeadContacts)
                {
                    lead.LeadContacts.Add(new LeadContact
                    {
                        Department = contact.Department,
                        Email = contact.Email,
                        Name = contact.Name,
                        Lead = lead,
                        PhoneNumber = contact.PhoneNumber
                    });
                }

                foreach(var activity in request.LeadCreateDto.Activities)
                {
                    lead.Activities.Add(new Activity
                    {
                        ActivityType = await _context.ActivityTypes.FirstOrDefaultAsync(x => x.Name == activity),
                        Lead = lead,
                        User = await _userRepository.GetUserByIdAsync(request.UserId)
                    });
                }

                lead = await _leadRepository.AddLeadAsync(lead);

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
