using CRM.Application.Dto;
using System.Threading.Tasks;

namespace CRM.Application.Interface
{
    public interface ICustomerService
    {
        Task AddNewCustomer(CustomerCreateDto model);

        Task ChangeCustomerStatus(int customerId, int statusId);

        Task EditCustomerData(CustomerEditDto model);

        Task<CustomersListDto> GetAllActiveCustomers();

        Task<CustomersListDto> GetAllCustomers();

        Task<CustomersListDto> GetCustomersWithStatus(int statusId);

        Task<CustomerViewDto> GetCustomer(int id);

        Task MakeCustomerInactive(int customerId);
    }
}