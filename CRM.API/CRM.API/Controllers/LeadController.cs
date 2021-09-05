using ApiApplication.DTO;
using ApiApplication.Lead.AddLead;
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

        [HttpPost("AddNewLead")]
        [Authorize]
        public async Task<IActionResult> AddNewLeadAsync(LeadCreateDto dto)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userId = claimsIdentity.Claims.ToList().FirstOrDefault(x => x.Type == "id").Value;
            var companyId = claimsIdentity.Claims.ToList().FirstOrDefault(x => x.Type == "companyId").Value;

            var command = new AddLeadCommand
            {
                UserId = userId,
                CompanyId=Convert.ToInt32(companyId),
                LeadCreateDto=dto
            };

            return await _mediator.Send(command);
        }
    }
}
