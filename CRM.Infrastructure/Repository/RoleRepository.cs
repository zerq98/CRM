using CRM.Domain.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Infrastructure.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDataContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<RoleRepository> _logger;

        public RoleRepository(AppDataContext context,
                              RoleManager<IdentityRole> roleManager,
                              ILogger<RoleRepository> logger)
        {
            _context = context;
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task<string> AddAsync(IdentityRole role)
        {
            await _roleManager.CreateAsync(role);

            return role.Id;
        }

        public IQueryable<IdentityRole> GetAllAsync()
        {
            return _context.Roles.AsQueryable();
        }

        public async Task<IdentityRole> GetById(string roleId)
        {
            return await _roleManager.FindByIdAsync(roleId);
        }

        public async Task RemoveRoleAsync(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);

            if (role != null)
            {
                await _roleManager.DeleteAsync(role);
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}