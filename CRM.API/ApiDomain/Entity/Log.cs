using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDomain.Entity
{
    public class Log
    {
        public int Id { get; set; }
        public string ModuleName { get; set; }
        public string LogMessage { get; set; }
    }
}
