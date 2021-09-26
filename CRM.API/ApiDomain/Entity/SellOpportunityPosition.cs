using System;

namespace ApiDomain.Entity
{
    public class SellOpportunityPosition
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public double NetValue { get; set; }
        public double Markup { get; set; }
        public double GrossValue { get; set; }
        public double VatValue { get; set; }
        public SellOpportunityHeader OpportunityHeader { get; set; }
        public int OpportunityHeaderId { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModificationDate { get; set; }
    }
}