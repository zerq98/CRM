using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApiApplication.Account.Register
{
    public class RegisterUserCommand : IRequest<IActionResult>
    {
        public RegisterData Data { get; set; }
    }
}