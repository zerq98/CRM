﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Desktop.Data.Models
{
    public class LoginResponse
    {
        public string Id { get; set; }
        public string Token { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
