using ApiDomain.Entity;
using ApiDomain.Interface;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiInfrastructure.Repository
{
    public class UserRepository : BaseRepository,IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(AppDbContext context,UserManager<ApplicationUser> userManager) : base(context)
        {
            _userManager = userManager;
        }

        public async Task AssignClaimsAsync(List<string> claims, string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);

                if (user != null)
                {
                    foreach(var claim in claims)
                    {
                        await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim(claim, claim));
                    }
                }
            }
            catch (Exception ex)
            {
                await _context.Logs.AddAsync(new Log
                {
                    LogMessage = ex.Message,
                    ModuleName = "UserRepository/AssignClaimsAsync"
                });
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ChangePasswordAsync(string oldPassword, string newPassword, string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);

                if (user != null)
                {
                    var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
                    if (result.Succeeded)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                await _context.Logs.AddAsync(new Log
                {
                    LogMessage = ex.Message,
                    ModuleName = "UserRepository/ChangePasswordAsync"
                });
                await _context.SaveChangesAsync();

                return false;
            }
        }

        public async Task<bool> CheckLoginDataAsync(string login, string password)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(login);

                if (user != null)
                {
                    return await _userManager.CheckPasswordAsync(user, password);
                }
                return false;
            }
            catch (Exception ex)
            {
                await _context.Logs.AddAsync(new Log
                {
                    LogMessage = ex.Message,
                    ModuleName = "UserRepository/CheckLoginDataAsync"
                });
                await _context.SaveChangesAsync();

                return false;
            }
        }

        public async Task<bool> ConfirmEmailAsync(ApplicationUser user, string token)
        {
            try
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);

                return result.Succeeded;
            }
            catch(Exception ex)
            {
                await _context.Logs.AddAsync(new Log
                {
                    LogMessage = ex.Message,
                    ModuleName = "UserRepository/ConfirmEmailAsync"
                });
                await _context.SaveChangesAsync();

                return false;
            }
        }

        public async Task<ApplicationUser> CreateUserAsync(ApplicationUser user,string password)
        {
            try
            {
                await _userManager.CreateAsync(user,password);

                return user;
            }
            catch (Exception ex)
            {
                await _context.Logs.AddAsync(new Log
                {
                    LogMessage = ex.Message,
                    ModuleName = "UserRepository/CreateUserAsync"
                });
                await _context.SaveChangesAsync();

                return null;
            }
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<ApplicationUser> GetUserByLoginAsync(string login)
        {
            return await _userManager.FindByNameAsync(login);
        }
    }
}
