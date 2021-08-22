﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiApplication.Account.DashboardData
{
    public class GetDashboardDataQuery : IRequest<IActionResult>
    {
        public string UserId { get; set; }
    }
}
