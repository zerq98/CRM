using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiApplication.Administration.RemoveUser
{
    public class RemoveUserCommand : IRequest<IActionResult>
    {
        public string UserId { get; set; }
        public string AdminId { get; set; }
    }
}
