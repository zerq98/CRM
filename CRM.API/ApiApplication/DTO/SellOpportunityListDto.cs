using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiApplication.DTO
{
    public class SellOpportunityListDto
    {
        public List<SellOpportunityForListDto> SellOpportunities { get; set; }
        public List<string> TraderList { get; set; }
    }
}
