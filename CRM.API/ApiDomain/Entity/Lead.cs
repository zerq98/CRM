using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDomain.Entity
{
    public class Lead
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NIP { get; set; }
        public string Regon { get; set; }
        public LeadStatus LeadStatus { get; set; }
        public int LeadStatusId { get; set; }
        public List<LeadContact> LeadContacts { get; set; }
        public LeadAddress LeadAddress { get; set; }
        public int LeadAddressId { get; set; }
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public Company Company { get; set; }
        public int CompanyId { get; set; }
        public List<Activity> Activities { get; set; }
    }
}
