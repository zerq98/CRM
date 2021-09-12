using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiApplication.DTO
{
    public class LeadListResponseDto
    {
        public List<LeadForListDto> Leads { get; set; }
        public List<string> CompanyTraders { get; set; }
    }
}
