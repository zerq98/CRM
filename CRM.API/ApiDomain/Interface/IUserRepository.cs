using ApiDomain.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiDomain.Interface
{
    public interface IUserRepository
    {
        Task<ApplicationUser> CreateUserAsync(ApplicationUser user, string password);

        Task AssignClaimsAsync(List<string> claims, string userId);

        Task<bool> CheckLoginDataAsync(string login, string password);

        Task<bool> ChangePasswordAsync(string oldPassword, string newPassword, string userId);

        Task<ApplicationUser> GetUserByEmailAsync(string email);

        Task<ApplicationUser> GetUserByLoginAsync(string login);

        Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user);

        Task<bool> ConfirmEmailAsync(ApplicationUser user, string token);

        Task<ApplicationUser> GetUserByIdAsync(string userId);

        Task DeleteUserAsync(string userId);

        Task<List<string>> GetUserClaims(string userId);

        Task<List<ApplicationUser>> GetCompanyTraders(int companyId);
        Task<ApplicationUser> GetUserByNameAsync(string name);
    }
}