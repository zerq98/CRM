using ApiApplication.Account.ConfirmEmail;
using ApiApplication.Account.Login;
using ApiApplication.Account.Register;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("RegisterCompany")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterCompany(RegisterData data)
        {
            var command = new RegisterUserCommand
            {
                Data = data
            };

            return await _mediator.Send(command);
        }

        [HttpPost("ConfirmEmail")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var command = new ConfirmEmailCommand
            {
                UserId = userId,
                Token = token
            };

            return await _mediator.Send(command);
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var command = new LoginUserCommand
            {
                Login = request.Login,
                Password = request.Password
            };

            return await _mediator.Send(command);
        }
    }
}