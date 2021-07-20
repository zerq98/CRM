using ApiDomain.Entity;
using System.Threading.Tasks;

namespace ApiDomain.Interface
{
    public interface ICompanyRepository
    {
        Task<Company> CreateCompanyAsync(Company company);

        Task DeleteCompanyAsync(int companyId);
    }
}