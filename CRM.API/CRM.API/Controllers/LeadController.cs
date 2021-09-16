using ApiApplication.DTO;
using ApiApplication.Lead.AddLead;
using ApiApplication.Lead.GetAllLeads;
using ApiApplication.Lead.GetLead;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeadController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeadController(IMediator mediator)
        {
            _mediator = mediator;

        }

        [HttpPost("Upsert")]
        [Authorize]
        public async Task<IActionResult> UpsertAsync(LeadForDetailsDto dto)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userId = claimsIdentity.Claims.ToList().FirstOrDefault(x => x.Type == "id").Value;
            var companyId = claimsIdentity.Claims.ToList().FirstOrDefault(x => x.Type == "companyId").Value;

            var command = new UpsertLeadCommand
            {
                UserId = userId,
                CompanyId=Convert.ToInt32(companyId),
                LeadCreateDto=dto
            };

            return await _mediator.Send(command);
        }

        [HttpPost("GetAllLeads")]
        [Authorize]
        public async Task<IActionResult> GetAllLeadsAsync(LeadFiltersDto filtersDto)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var companyId = claimsIdentity.Claims.ToList().FirstOrDefault(x => x.Type == "companyId").Value;

            var command = new GetAllLeadsQuery
            {
                CompanyId = Convert.ToInt32(companyId),
                Filters = filtersDto
            };

            return await _mediator.Send(command);
        }

        [HttpGet("GetLead")]
        [Authorize]
        public async Task<IActionResult> GetLeadAsync(int leadId)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userId = claimsIdentity.Claims.ToList().FirstOrDefault(x => x.Type == "id").Value;
            var companyId = claimsIdentity.Claims.ToList().FirstOrDefault(x => x.Type == "companyId").Value;

            var command = new GetLeadQuery
            {
                CompanyId = Convert.ToInt32(companyId),
                Id=leadId,
                UserId=userId
            };

            return await _mediator.Send(command);
        }
    }
}
