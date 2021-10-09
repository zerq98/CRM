using ApiApplication.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiApplication.Administration.UpdateCompany
{
    public class UpdateCompanyCommand : IRequest<IActionResult>
    {
        public string UserId { get; set; }
        public CompanyDataDto Dto { get; set; }
    }
}
