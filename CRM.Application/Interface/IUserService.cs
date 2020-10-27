using CRM.Application.Dto.User;
using CRM.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Application.Interface
{
    public interface IUserService
    {
        Task<string> AddUserAsync(ApplicationUserCreateVM user);

        Task AssignUserToRoleAsync(string userId, List<UserRolesVM> roles);

        Task<string> EditUserAsync(ApplicationUserEditVM model);

        Task RemoveUser(string id);

        Task<ApplicationUserVM> GetApplicationUser(string id);

        Task<List<ApplicationUserVM>> GetAllUsers();
    }
}
