using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiApplication.DTO
{
    public class ProductFiltersDto
    {
        public string Name { get; set; }
        public string VatRate { get; set; }
        public string MarkupRate { get; set; }
    }
}
