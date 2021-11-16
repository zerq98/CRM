using ApiApplication.Helpers;
using ApiDomain.Interface;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApiApplication.Administration.RemoveUser
{
    public class RemoveUserHandler : IRequestHandler<RemoveUserCommand,IActionResult>
    {
        private readonly IBaseRepository _baseRepository;
        private readonly IUserRepository _userRepository;

        public RemoveUserHandler(IBaseRepository baseRepository,IUserRepository userRepository)
        {
            _baseRepository = baseRepository;
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
        {
            using(var trans = _baseRepository.GetTransaction())
            {
                try
                {
                    var user = await _userRepository.GetUserByIdAsync(request.UserId);

                    if (user != null)
                    {
                        await _userRepository.DeleteUserAsync(request.UserId);

                        await trans.CommitAsync();

                        return new JsonResult(new ApiResponse<object>
                        {
                            Code = 200,
                            Data = null,
                            ErrorMessage = ""
                        });
                    }

                    return new JsonResult(new ApiResponse<object>
                    {
                        Code = 404,
                        Data = null,
                        ErrorMessage = "Nie znaleziono użytkownika w bazie danych."
                    });
                }
                catch(Exception ex)
                {
                    return new JsonResult(new ApiResponse<object>
                    {
                        Code = 500,
                        Data = null,
                        ErrorMessage = "Nastąpił problem z serwerem, skontaktuj się z działem IT."
                    });
                }
            }
        }
    }
}
