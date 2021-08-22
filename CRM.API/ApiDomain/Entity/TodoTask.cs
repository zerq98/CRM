using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDomain.Entity
{
    public class TodoTask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime TaskDate { get; set; }
        public bool IsFinished { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
