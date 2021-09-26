using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiApplication.DTO
{
    public class OpportunitiesFiltersDto
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string Status { get; set; }
        public string Trader { get; set; }
        public string LeadName { get; set; }
    }
}
