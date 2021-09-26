using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiApplication.SellOpportunity.GetOpportunity
{
    public class GetOpportunityQuery : IRequest<IActionResult>
    {
        public int OpportunityId { get; set; }
        public int CompanyId { get; set; }
    }
}
