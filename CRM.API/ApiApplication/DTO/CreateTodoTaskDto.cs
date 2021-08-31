using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiApplication.DTO
{
    public class CreateTodoTaskDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime TaskDate { get; set; }
        public string UserId { get; set; }
    }
}
