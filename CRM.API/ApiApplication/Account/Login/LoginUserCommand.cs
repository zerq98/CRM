using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApiApplication.Account.Login
{
    public class LoginUserCommand : IRequest<IActionResult>
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}