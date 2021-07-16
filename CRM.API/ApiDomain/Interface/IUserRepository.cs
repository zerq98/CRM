using ApiDomain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDomain.Interface
{
    public interface IUserRepository
    {
        Task<ApplicationUser> CreateUserAsync(ApplicationUser user,string password);
        Task AssignClaimsAsync(List<string> claims, string userId);
        Task<bool> CheckLoginDataAsync(string login, string password);
        Task<bool> ChangePasswordAsync(string oldPassword, string newPassword, string userId);
        Task<ApplicationUser> GetUserByEmailAsync(string email);
        Task<ApplicationUser> GetUserByLoginAsync(string login);
        Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user);
        Task<bool> ConfirmEmailAsync(ApplicationUser user, string token);
    }
}
