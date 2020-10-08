using CRM.Domain.Entity;
using CRM.Domain.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Infrastructure.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDataContext _context;
        private readonly ILogger<CustomerRepository> _logger;

        public CustomerRepository(AppDataContext context,
                                 ILogger<CustomerRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<int> AddAsync(Customer entity)
        {
            if (entity != null)
            {
                await _context.Customers.AddAsync(entity);
            }

            return entity.Id;
        }

        public async Task EditCustomer(Customer model)
        {
            if (model != null)
            {
                _context.Customers.Update(model);
                await _context.SaveChangesAsync();
            }
        }

        public IQueryable<Customer> GetAll()
        {
            return _context.Customers.AsQueryable();
        }

        public IQueryable<Customer> GetAllActive()
        {
            return _context.Customers.Where(x => x.IsActive).AsQueryable();
        }

        public async Task<Customer> GetById(int id)
        {
            return await _context.Customers.SingleOrDefaultAsync(x => x.Id == id);
        }

        public IQueryable<Customer> GetCustomerWithStatus(int statusId)
        {
            return _context.Customers.Where(x => x.StatusId == statusId && x.IsActive);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}