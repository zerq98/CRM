using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiApplication.Lead.GetLead
{
    public class GetLeadQuery : IRequest<IActionResult>
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
    }
}
