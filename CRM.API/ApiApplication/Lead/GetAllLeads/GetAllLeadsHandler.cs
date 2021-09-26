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

namespace ApiApplication.Lead.GetAllLeads
{
    public class GetAllLeadsHandler : IRequestHandler<GetAllLeadsQuery, IActionResult>
    {
        private readonly ILeadRepository _leadRepository;
        private readonly IUserRepository _userRepository;

        public GetAllLeadsHandler(ILeadRepository leadRepository,IUserRepository userRepository)
        {
            _leadRepository = leadRepository;
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Handle(GetAllLeadsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var dbLeads = await _leadRepository.GetAllLeadsAsync(request.CompanyId,request.Filters.DateFrom,request.Filters.DateTo);
                var traders = await _userRepository.GetCompanyTraders(request.CompanyId);
                var response = new LeadListResponseDto()
                {
                    CompanyTraders = new List<string>(),
                    Leads = new List<LeadForListDto>()
                };

                foreach(var trader in traders)
                {
                    response.CompanyTraders.Add(trader.FirstName + " " + trader.LastName);
                }

                foreach(var lead in dbLeads)
                {
                    response.Leads.Add(new LeadForListDto
                    {
                        Id = lead.Id,
                        Email = lead.LeadContacts.Count>0?lead.LeadContacts.First().Email:"",
                        User = lead.User.FirstName + " " + lead.User.LastName,
                        MainContact = lead.LeadContacts.Count > 0 ? lead.LeadContacts.First().Name:"",
                        Name = lead.Name,
                        CreateDate = lead.CreateDate,
                        Status=lead.LeadStatus.Name
                    });
                }

                if (request.Filters.Name != "")
                {
                    response.Leads = response.Leads.Where(x => x.Name.Contains(request.Filters.Name)).ToList();
                }
                if (request.Filters.Status != "")
                {
                    response.Leads = response.Leads.Where(x => x.Status == request.Filters.Status).ToList();
                }
                if (request.Filters.User != "")
                {
                    response.Leads = response.Leads.Where(x => x.User.Contains(request.Filters.User)).ToList();
                }
                if(request.Filters.Email != "")
                {
                    response.Leads = response.Leads.Where(x => x.Email.Contains(request.Filters.Email)).ToList();
                }

                return new JsonResult(new ApiResponse<LeadListResponseDto>
                {
                    Data = response,
                    Code = 200,
                    ErrorMessage = ""
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
