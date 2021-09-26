using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiApplication.DTO
{
    public class SellOpportunityDetailsResponseDto
    {
        public SellOpportunityDetailsDto SellOpportunity { get; set; }
        public List<string> Leads { get; set; }
        public List<string> CompanyTraders { get; set; }
        public List<ProductForListDto> Products { get; set; }
    }
}
