using ApiApplication.Helpers;
using ApiApplication.Helpers.JWT;
using ApiDomain.Interface;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiApplication.Account.Login
{
    public class LoginUserHandler : IRequestHandler<LoginUserCommand, IActionResult>
    {
        private IUserRepository _userRepository;
        private JwtConfig _config;

        public LoginUserHandler(IUserRepository userRepository, IOptionsMonitor<JwtConfig> config)
        {
            _userRepository = userRepository;
            _config = config.CurrentValue;
        }

        public async Task<IActionResult> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var expireDate = DateTime.Now.AddDays(1);

                if (await _userRepository.CheckLoginDataAsync(request.Login, request.Password))
                {
                    var user = await _userRepository.GetUserByLoginAsync(request.Login);
                    var token = TokenGenerator.Generate(user,
                        await _userRepository.GetUserClaims(user.Id), _config, expireDate);

                    return new JsonResult(new ApiResponse<AuthorizeResponse>
                    {
                        Data = new AuthorizeResponse { Id = user.Id, Token = token, ExpireDate = expireDate },
                        Code = 200,
                        ErrorMessage = ""
                    });
                }

                return new JsonResult(new ApiResponse<object>
                {
                    Data = null,
                    Code = 401,
                    ErrorMessage = "Dane logowania są nie poprawne."
                });
            }
            catch
            {
                return new JsonResult(new ApiResponse<object>
                {
                    Data = null,
                    Code = 500,
                    ErrorMessage = "Nastąpił problem z serwerem, skontaktuj się z działem IT."
                });
            }
        }
    }
}