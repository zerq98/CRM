using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiApplication.Administration.GetUserData
{
    public class GetUserDataQuery : IRequest<IActionResult>
    {
        public string RequestUserId { get; set; }
        public string UserId { get; set; }
    }
}
