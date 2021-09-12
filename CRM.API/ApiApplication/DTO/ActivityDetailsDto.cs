using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiApplication.DTO
{
    public class ActivityDetailsDto
    {
        public int Id { get; set; }
        public string ActivityType { get; set; }
        public string User { get; set; }
        public DateTime ActivityDate { get; set; }
        public bool Deleted { get; set; }
        public int LocalId { get; set; }
    }
}
