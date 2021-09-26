using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiApplication.DTO
{
    public class SellOpportunityPositionDetailsDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public double NetValue { get; set; }
        public double Markup { get; set; }
        public double GrossValue { get; set; }
        public double VatValue { get; set; }
        public string Product { get; set; }
        public int LocalId { get; set; }
        public bool Deleted { get; set; }
        public string UnitOfMeasurement { get; set; }
    }
}
