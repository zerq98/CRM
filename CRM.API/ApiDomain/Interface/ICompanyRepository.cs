using ApiDomain.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiDomain.Interface
{
    public interface ICompanyRepository
    {
        Task<Company> CreateCompanyAsync(Company company);

        Task DeleteCompanyAsync(int companyId);
        Task<Company> GetByIdAsync(int companyId);
    }
}