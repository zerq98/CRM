using ApiApplication.DTO;
using ApiApplication.SellOpportunity.GetAllOpportunities;
using ApiApplication.SellOpportunity.GetOpportunity;
using ApiApplication.SellOpportunity.UpsertOpportunity;
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
    public class OpportunityController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OpportunityController(IMediator mediator)
        {
            _mediator = mediator;

        }

        [HttpPost("GetAllOpportunities")]
        [Authorize]
        public async Task<IActionResult> GetAllOpportunitiesAsync(OpportunitiesFiltersDto dto)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var companyId = Convert.ToInt32(claimsIdentity.Claims.ToList().FirstOrDefault(x => x.Type == "companyId").Value);

            var command = new GetAllOpportunitiesQuery
            {
                CompanyId = companyId,
                Filters = dto,
                GetOrders = false
            };

            return await _mediator.Send(command);
        }

        [HttpPost("GetAllOrders")]
        [Authorize]
        public async Task<IActionResult> GetAllOrdersAsync(OpportunitiesFiltersDto dto)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var companyId = Convert.ToInt32(claimsIdentity.Claims.ToList().FirstOrDefault(x => x.Type == "companyId").Value);

            var command = new GetAllOpportunitiesQuery
            {
                CompanyId = companyId,
                Filters = dto,
                GetOrders = true
            };

            return await _mediator.Send(command);
        }

        [HttpGet("GetOpportunity")]
        [Authorize]
        public async Task<IActionResult> GetOpportunity(int opportunityId)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var companyId = Convert.ToInt32(claimsIdentity.Claims.ToList().FirstOrDefault(x => x.Type == "companyId").Value);

            var command = new GetOpportunityQuery
            {
                CompanyId = companyId,
                OpportunityId=opportunityId
            };

            return await _mediator.Send(command);
        }

        [HttpPost("UpsertOpportunity")]
        [Authorize]
        public async Task<IActionResult> UpsertOpportunity(SellOpportunityDetailsDto dto)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userId = claimsIdentity.Claims.ToList().FirstOrDefault(x => x.Type == "id").Value;
            var companyId = Convert.ToInt32(claimsIdentity.Claims.ToList().FirstOrDefault(x => x.Type == "companyId").Value);

            var command = new UpsertOpportunityCommand
            {
                CompanyId = companyId,
                SellOpportunity=dto,
                UserId=userId
            };

            return await _mediator.Send(command);
        }
    }
}
