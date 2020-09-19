using CRM.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.Interface
{
    public interface ICompanyRepository : IBaseRepository<Company>
    {
        Task<List<ApplicationUser>> GetAllEmployeesForCompanyAsync(int companyId);

        Task<string> AddEmployee(int companyId, ApplicationUser user);

        Task RemoveEmployee(string employeeId);

        Task<ApplicationUser> GetUserById(string userId);
    }
}
