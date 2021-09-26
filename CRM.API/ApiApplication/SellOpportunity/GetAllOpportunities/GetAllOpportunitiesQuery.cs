using ApiApplication.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiApplication.SellOpportunity.GetAllOpportunities
{
    public class GetAllOpportunitiesQuery : IRequest<IActionResult>
    {
        public int CompanyId { get; set; }
        public OpportunitiesFiltersDto Filters { get; set; }
        public bool GetOrders { get; set; }
    }
}
