using ApiApplication.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiApplication.Lead.GetAllLeads
{
    public class GetAllLeadsQuery : IRequest<IActionResult>
    {
        public int CompanyId { get; set; }
        public LeadFiltersDto Filters { get; set; }
        public string UserId { get; set; }
    }
}
