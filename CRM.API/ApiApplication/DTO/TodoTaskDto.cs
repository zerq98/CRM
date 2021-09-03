using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiApplication.DTO
{
    public class TodoTaskDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Completed { get; set; }
        public string Description { get; set; }
        public string TaskRange { get; set; }
        public DateTime TaskDate { get; set; }
    }
}
