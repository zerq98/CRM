using ApiApplication.Account.ConfirmEmail;
using ApiApplication.Account.DashboardData;
using ApiApplication.Account.Login;
using ApiApplication.Account.Register;
using ApiApplication.Helpers;
using CRM.API.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        [HttpPost("ProfilePicture"),RequestSizeLimit(3145728)]
        public async Task<IActionResult> UploadPicture()
        {
            var response = new ApiResponse<object>();

            if (FileUploader.UploadFile(Request.Form.Files[0]))
            {
                response.Code = 201;
                response.Data = null;
                response.ErrorMessage = "";
            }
            else
            {
                response.Code = 500;
                response.Data = null;
                response.ErrorMessage = "Nie udało się zaktualizować zdjęcia";
            }

            return new JsonResult(response);
        }

        [HttpGet("DashboardInfo")]
        [Authorize]
        public async Task<IActionResult> GetDashboardData(string id)
        {
            var command = new GetDashboardDataQuery
            {
                UserId=id
            };

            return await _mediator.Send(command);
        }

    }
}