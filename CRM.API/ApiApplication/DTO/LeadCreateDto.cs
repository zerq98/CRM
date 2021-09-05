using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiApplication.DTO
{
    public class LeadCreateDto
    {
        public string Name { get; set; }
        public string Regon { get; set; }
        public string NIP { get; set; }
        public string LeadStatus { get; set; }
        public List<CreateLeadContectDto> LeadContacts { get; set; }
        public CreateLeadAddressDto LeadAddress { get; set; }
        public List<string> Activities { get; set; }
    }
}
