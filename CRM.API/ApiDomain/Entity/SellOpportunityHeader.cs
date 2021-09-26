using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDomain.Entity
{
    public class SellOpportunityHeader
    {
        public int Id { get; set; }
        public Company Company { get; set; }
        public int CompanyId { get; set; }
        public Lead Lead { get; set; }
        public int LeadId { get; set; }
        public OpportunityStatus Status { get; set; }
        public int StatusId { get; set; }
        public ApplicationUser Trader { get; set; }
        public string TraderId { get; set; }
        public double SumNetValue { get; set; }
        public double SumGrossValue { get; set; }
        public double SumMarkupValue { get; set; }
        public double SumVatValue { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public List<SellOpportunityPosition> Positions { get; set; }
    }
}
