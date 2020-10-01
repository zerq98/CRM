using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Domain.Interface
{
    public interface IRoleRepository
    {
        Task<string> AddAsync(IdentityRole role);

        Task RemoveRoleAsync(string roleId);

        IQueryable<IdentityRole> GetAllAsync();

        Task SaveAsync();

        Task<IdentityRole> GetById(string id);
    }
}