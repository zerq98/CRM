using CRM.Application.Dto.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Application.Interface
{
    public interface IRoleService
    {
        Task<string> CreateRoleAsync(CreateRoleVm model);

        Task RemoveRoleAsync(string id);

        Task<List<UserRolesVM>> GetRolesAsync();
    }
}
