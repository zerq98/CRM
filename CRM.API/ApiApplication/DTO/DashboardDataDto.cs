using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiApplication.DTO
{
    public class DashboardDataDto
    {
        public string Name { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public List<DashboardTodoTaskDto> TodoTasks { get; set; }
    }
}
