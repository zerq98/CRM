using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiApplication.DTO
{
    public class LeadDetailsDto
    {
        public LeadForDetailsDto Lead { get; set; }
        public List<string> CompanyTraders { get; set; }
        public string User { get; set; }
    }
}
