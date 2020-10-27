using CRM.Application.Dto;
using CRM.Application.Dto.Customer;
using System.Threading.Tasks;

namespace CRM.Application.Interface
{
    public interface ICustomerService
    {
        Task<int> AddNewCustomer(CustomerCreateDto model);

        Task ChangeCustomerStatus(int customerId, int statusId);

        Task EditCustomerData(CustomerEditDto model);

        Task<CustomersListDto> GetAllActiveCustomers(string userName);

        Task<CustomersListDto> GetAllCustomers();

        Task<CustomersListDto> GetCustomersWithStatus(int statusId);

        Task<CustomerViewDto> GetCustomer(int id);

        Task MakeCustomerInactive(int customerId);
    }
}