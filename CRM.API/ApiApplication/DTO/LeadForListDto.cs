using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiApplication.DTO
{
    public class LeadForListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public string Email { get; set; }
        public string MainContact { get; set; }
        public string User { get; set; }
        public string Status { get; set; }
    }
}
