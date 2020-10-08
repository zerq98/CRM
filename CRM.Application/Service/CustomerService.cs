using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRM.Application.Dto;
using CRM.Application.Interface;
using CRM.Domain.Entity;
using CRM.Domain.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CRM.Application.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepostiory;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository customerRepository,
                               IMapper mapper)
        {
            _customerRepostiory = customerRepository;
            _mapper = mapper;
        }

        public async Task<int> AddNewCustomer(CustomerCreateDto model)
        {
            var customer = _mapper.Map<Customer>(model);

            return await _customerRepostiory.AddAsync(customer);
        }

        public async Task ChangeCustomerStatus(int customerId, int statusId)
        {
            var customer = await _customerRepostiory.GetById(customerId);
            customer.StatusId = statusId;
            await _customerRepostiory.EditCustomer(customer);
            await _customerRepostiory.SaveAsync();
        }

        public async Task EditCustomerData(CustomerEditDto model)
        {
            var customer = await _customerRepostiory.GetById(model.Id);

            customer.ModificationDate = DateTime.Now;
            customer.DealSize = model.DealSize;
            customer.AddressDetails = _mapper.Map<CustomerAddressDetails>(model.customerAddress);
            customer.Description = model.Description;
            customer.Name = model.Name;

            await _customerRepostiory.EditCustomer(customer);
            await _customerRepostiory.SaveAsync();
        }

        public async Task<CustomersListDto> GetAllActiveCustomers()
        {
            var customers = await _customerRepostiory.GetAllActive()
                            .ProjectTo<CustomerViewDto>(_mapper.ConfigurationProvider)
                            .ToListAsync();

            return new CustomersListDto
            {
                Customers = customers,
                Count = customers.Count
            };
        }

        public async Task<CustomersListDto> GetAllCustomers()
        {
            var customers = await _customerRepostiory.GetAll()
                            .ProjectTo<CustomerViewDto>(_mapper.ConfigurationProvider)
                            .ToListAsync();

            return new CustomersListDto
            {
                Customers = customers,
                Count = customers.Count
            };
        }

        public async Task<CustomerViewDto> GetCustomer(int id)
        {
            return _mapper.Map<CustomerViewDto>(await _customerRepostiory.GetById(id));
        }

        public async Task<CustomersListDto> GetCustomersWithStatus(int statusId)
        {
            var customers = await _customerRepostiory.GetCustomerWithStatus(statusId)
                            .ProjectTo<CustomerViewDto>(_mapper.ConfigurationProvider)
                            .ToListAsync();

            return new CustomersListDto
            {
                Customers = customers,
                Count = customers.Count
            };
        }

        public async Task MakeCustomerInactive(int customerId)
        {
            var customer = await _customerRepostiory.GetById(customerId);
            customer.IsActive = false;
            await _customerRepostiory.EditCustomer(customer);
            await _customerRepostiory.SaveAsync();
        }
    }
}