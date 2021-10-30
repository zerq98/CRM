using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Desktop.Data.Models
{
    public class AdministrationDataDto
    {
        public List<UserForAdministrationDto> Users { get; set; }
        public CompanyStatisticsDto Statistics { get; set; }
        public CompanyDataDto Company { get; set; }
    }
}
