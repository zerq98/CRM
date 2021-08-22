using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TodoTaskController : Controller
    {
        private readonly IMediator _mediator;

        public TodoTaskController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut("MarkAsCompleted")]
        public async Task<IActionResult> MarkAsCompleted(int todoTaskId)
        {
            //var command = new GetDashboardDataQuery
            //{
            //    UserId = id
            //};

            //return await _mediator.Send(command);

            return null;
        }
    }
}
