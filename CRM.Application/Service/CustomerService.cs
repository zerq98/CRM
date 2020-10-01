using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRM.Application.Dto;
using CRM.Application.Interface;
using CRM.Domain.Entity;
using CRM.Domain.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
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
        public async Task AddNewCustomer(CustomerCreateDto model)
        {
            var customer = _mapper.Map<Customer>(model);

            await _customerRepostiory.AddAsync(customer);
        }

        public async Task ChangeCustomerStatus(int customerId, int statusId)
        {
            var customer = await _customerRepostiory.GetById(customerId);
            customer.StatusId = statusId;
            await _customerRepostiory.EditCustomer(customer);
            await _customerRepostiory.SaveAsync();
        }

        public Task EditCustomerData(CustomerEditDto model)
        {
            throw new NotImplementedException();
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
