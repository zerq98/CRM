using ApiDomain.Interface;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace ApiApplication.Account.ConfirmEmail
{
    public class ConfirmEmailHandler : IRequestHandler<ConfirmEmailCommand, IActionResult>
    {
        private IUserRepository _userRepository;

        public ConfirmEmailHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(request.UserId);
                if (user != null)
                {
                    await _userRepository.ConfirmEmailAsync(user, request.Token);

                    return new StatusCodeResult(200);
                }

                return new StatusCodeResult(404);
            }
            catch
            {
                return new StatusCodeResult(500);
            }
        }
    }
}