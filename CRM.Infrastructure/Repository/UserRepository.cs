using CRM.Domain.Entity;
using CRM.Domain.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDataContext _context;

        public UserRepository(UserManager<ApplicationUser> userManager,
                              RoleManager<IdentityRole> roleManager,
                              AppDataContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<string> AddUserAsync(ApplicationUser user,string password)
        {
            await _userManager.CreateAsync(user,password);
            return user.Id;
        }

        public async Task AssignUserToRoleAsync(string userId, List<string> roles)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.RemoveFromRolesAsync(user, await _userManager.GetRolesAsync(user));
            if (result.Succeeded)
            {
                await _userManager.AddToRolesAsync(user, roles);
            }
        }

        public async Task<string> EditUserAsync(ApplicationUser model)
        {
            await _userManager.UpdateAsync(model);
            return model.Id;
        }

        public async Task RemoveUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            user.IsActive = false;
            await _userManager.UpdateAsync(user);
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<List<IdentityUser>> GetApplicationUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<List<string>> GetUserRoles(string userId)
        {
            return (await _userManager.GetRolesAsync(await GetUserByIdAsync(userId))).ToList();
        }

        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<bool> CheckPasswordAsync(ApplicationUser user,string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }
    }
}
