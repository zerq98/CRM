using ApiApplication.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiApplication.Administration.UpsertUser
{
    public class UpsertUserCommand : IRequest<IActionResult>
    {
        public UserUpsertDto Dto { get; set; }
        public int CompanyId { get; set; }
    }
}
