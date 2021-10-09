using ApiApplication.Administration.GetAdministrationData;
using ApiApplication.Administration.GetUserData;
using ApiApplication.Administration.UpdateCompany;
using ApiApplication.Administration.UpsertUser;
using ApiApplication.DTO;
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
    public class AdministrationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdministrationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetCompanyData")]
        [Authorize]
        public async Task<IActionResult> GetCompanyData()
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userId = claimsIdentity.Claims.ToList().FirstOrDefault(x => x.Type == "id").Value;
            var companyId = claimsIdentity.Claims.ToList().FirstOrDefault(x => x.Type == "companyId").Value;

            var command = new GetAdministrationDataQuery
            {
                CompanyId = Convert.ToInt32(companyId),
                UserId = userId
            };

            return await _mediator.Send(command);
        }

        [HttpGet("GetUserData")]
        [Authorize]
        public async Task<IActionResult> GetUserData(string userId)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var requestUserId = claimsIdentity.Claims.ToList().FirstOrDefault(x => x.Type == "id").Value;
            var companyId = claimsIdentity.Claims.ToList().FirstOrDefault(x => x.Type == "companyId").Value;

            var command = new GetUserDataQuery
            {
                RequestUserId=requestUserId,
                UserId = userId
            };

            return await _mediator.Send(command);
        }

        [HttpPost("UpsertUser")]
        [Authorize]
        public async Task<IActionResult> UpsertUserAsync(UserUpsertDto dto)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userId = claimsIdentity.Claims.ToList().FirstOrDefault(x => x.Type == "id").Value;
            var companyId = claimsIdentity.Claims.ToList().FirstOrDefault(x => x.Type == "companyId").Value;

            var command = new UpsertUserCommand
            {
                CompanyId = Convert.ToInt32(companyId),
                Dto = dto
            };

            return await _mediator.Send(command);
        }

        [HttpPost("UpdateCompanyData")]
        [Authorize]
        public async Task<IActionResult> UpdateCompanyAsync(CompanyDataDto dto)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userId = claimsIdentity.Claims.ToList().FirstOrDefault(x => x.Type == "id").Value;

            var command = new UpdateCompanyCommand
            {
                Dto = dto,
                UserId = userId
            };

            return await _mediator.Send(command);
        }
    }
}
