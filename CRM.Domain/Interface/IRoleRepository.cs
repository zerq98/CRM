using CRM.Domain.Entity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.Interface
{
    public interface IRoleRepository
    {

        Task<string> AddAsync(IdentityRole role);

        Task RemoveRoleAsync(string roleId);

        Task<List<IdentityRole>> GetAllAsync();

        Task SaveAsync();

        Task<IdentityRole> GetById(string id);
    }
}
