using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiApplication.DTO
{
    public class ProductForListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double VatRate { get; set; }
        public double UnitValue { get; set; }
        public double MarkupRate { get; set; }
        public string UnitOfMeasurement { get; set; }
    }
}
