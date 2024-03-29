﻿using System;
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
        public List<TodoTaskDto> TodoTasks { get; set; }
        public List<int> UserActivity { get; set; }
        public List<int> SalesData { get; set; }
    }
}
