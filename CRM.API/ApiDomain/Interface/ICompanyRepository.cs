using ApiDomain.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiDomain.Interface
{
    public interface ICompanyRepository
    {
        Task<Company> CreateCompanyAsync(Company company);
        Task<Company> GetByIdAsync(int companyId);
        Task<Company> GetByNameAsync(string name);
        Task UpdateCompanyAsync(Company company);
    }
}