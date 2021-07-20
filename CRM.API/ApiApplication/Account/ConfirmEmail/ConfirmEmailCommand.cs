using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApiApplication.Account.ConfirmEmail
{
    public class ConfirmEmailCommand : IRequest<IActionResult>
    {
        public string UserId { get; set; }
        public string Token { get; set; }
    }
}