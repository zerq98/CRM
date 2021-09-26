using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiApplication.DTO
{
    public class SellOpportunityDetailsDto
    {
        public int Id { get; set; }
        public string Lead { get; set; }
        public string Status { get; set; }
        public string Trader { get; set; }
        public List<SellOpportunityPositionDetailsDto> Positions { get; set; }
    }
}
