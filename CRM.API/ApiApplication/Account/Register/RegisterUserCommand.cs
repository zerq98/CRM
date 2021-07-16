using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiApplication.Account.Register
{
    public class RegisterUserCommand : IRequest<IActionResult>
    {
        public RegisterData Data { get; set; }
    }
}
