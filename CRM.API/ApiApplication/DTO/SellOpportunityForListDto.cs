using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiApplication.DTO
{
    public class SellOpportunityForListDto
    {
        public int Id { get; set; }
        public string Lead { get; set; }
        public string Status { get; set; }
        public string Trader { get; set; }
        public double SumNetValue { get; set; }
        public double SumGrossValue { get; set; }
        public double SumMarkupValue { get; set; }
        public double SumVatValue { get; set; }
        public List<SellOpportunityPositionForListDto> Positions { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
