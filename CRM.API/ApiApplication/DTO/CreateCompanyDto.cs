using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiApplication.DTO
{
    public class CreateCompanyDto
    {
        public string CompanyName { get; set; }
        public string NIP { get; set; }
        public string Regon { get; set; }
    }
}
