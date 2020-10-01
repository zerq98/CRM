using CRM.Domain.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Domain.Interface
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        IQueryable<Customer> GetAllActive();

        IQueryable<Customer> GetCustomerWithStatus(int statusId);

        Task EditCustomer(Customer model);
    }
}