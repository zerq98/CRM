using CRM.Application.Dto.User;
using CRM.Application.Interface;
using CRM.Domain.Entity;
using CRM.Domain.Interface;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Application.Service
{
    public class AccountService : IAccountService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserRepository _userRepository;

        public AccountService(SignInManager<ApplicationUser> signInManager,
                              IUserRepository userRepository)
        {
            _signInManager = signInManager;
            _userRepository = userRepository;
        }

        public async Task<string> LoginAsync(LoginDto model)
        {
            var user = await _userRepository.GetUserByEmailAsync(model.Email);

            if(user!=null && await _userRepository.CheckPasswordAsync(user, model.Password))
            {
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, true);
                if (result.Succeeded)
                {
                    return "Logged";
                }else if (result.IsLockedOut)
                {
                    return "Locked";
                }
            }

            return "Error";
        }
    }
}
