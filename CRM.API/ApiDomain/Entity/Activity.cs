using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDomain.Entity
{
    public class Activity
    {
        public int Id { get; set; }
        public ActivityType ActivityType { get; set; }
        public int ActivityTypeId { get; set; }
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public Lead Lead { get; set; }
        public int LeadId { get; set; }
    }
}
