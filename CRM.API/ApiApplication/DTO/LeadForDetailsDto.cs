using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiApplication.DTO
{
    public class LeadForDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NIP { get; set; }
        public string Regon { get; set; }
        public string LeadStatus { get; set; }
        public List<LeadContactDto> LeadContacts { get; set; }
        public LeadAddressDto LeadAddress { get; set; }
        public string User { get; set; }
        public List<ActivityDetailsDto> Activities { get; set; }
    }
}
