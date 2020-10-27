using CRM.Domain.Entity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.Interface
{
    public interface IUserRepository
    {
        Task<string> AddUserAsync(ApplicationUser user,string password);

        Task AssignUserToRoleAsync(string userId, List<string> roles);

        Task<string> EditUserAsync(ApplicationUser model);

        Task RemoveUser(string id);
        Task<ApplicationUser> GetUserByIdAsync(string id);

        Task<List<IdentityUser>> GetApplicationUsers();

        Task<List<string>> GetUserRoles(string userId);

        Task<ApplicationUser> GetUserByEmailAsync(string email);

        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
    }
}
