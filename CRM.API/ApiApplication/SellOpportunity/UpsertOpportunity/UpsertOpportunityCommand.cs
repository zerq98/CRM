using ApiApplication.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiApplication.SellOpportunity.UpsertOpportunity
{
    public class UpsertOpportunityCommand : IRequest<IActionResult>
    {
        public string UserId { get; set; }
        public int CompanyId { get; set; }
        public SellOpportunityDetailsDto SellOpportunity { get; set; }
    }
}
