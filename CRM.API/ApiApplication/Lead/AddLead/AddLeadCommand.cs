using ApiApplication.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiApplication.Lead.AddLead
{
    public class AddLeadCommand : IRequest<IActionResult>
    {
        public LeadCreateDto LeadCreateDto { get; set; }
        public int CompanyId { get; set; }
        public string UserId { get; set; }
    }
}
