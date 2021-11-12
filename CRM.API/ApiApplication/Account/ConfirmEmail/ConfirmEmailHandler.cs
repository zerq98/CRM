using ApiApplication.Helpers;
using ApiDomain.Interface;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
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
                    request.Token = request.Token.Replace(" ", "+");
                    var result = await _userRepository.ConfirmEmailAsync(user, request.Token);

                    if (result)
                    {
                        return new JsonResult(
                        new ApiResponse<object>
                        {
                            Code = 200,
                            Data = null,
                            ErrorMessage = ""
                        });
                    }
                }

                return new JsonResult(
                        new ApiResponse<object>
                        {
                            Code = 404,
                            Data = null,
                            ErrorMessage = "Nie znaleziono użytkownika o takim Id"
                        });
            }
            catch(Exception ex)
            {
                return new JsonResult(
                        new ApiResponse<object>
                        {
                            Code = 500,
                            Data = null,
                            ErrorMessage = "Coś poszło nie tak. Skontaktuj się z administratorem."
                        });
            }
        }
    }
}