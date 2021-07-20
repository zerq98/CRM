using ApiApplication.Helpers.JWT;
using ApiDomain.Interface;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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
                if (await _userRepository.CheckLoginDataAsync(request.Login, request.Password))
                {
                    var user = await _userRepository.GetUserByLoginAsync(request.Login);
                    var token = TokenGenerator.Generate(user,
                        await _userRepository.GetUserClaims(user.Id), _config);

                    return new JsonResult(new AuthorizeResponse { Id = user.Id, Token = token });
                }

                return new StatusCodeResult(401);
            }
            catch
            {
                return new StatusCodeResult(500);
            }
        }
    }
}