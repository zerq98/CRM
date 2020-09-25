using CRM.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.Interface
{
    public interface ICompanyRepository : IBaseRepository<Customer>
    {
        Task<List<ApplicationUser>> GetAllEmployeesForCompanyAsync(int companyId);

        Task RemoveEmployee(string employeeId);

        Task<ApplicationUser> GetUserById(string userId);
    }
}
