using CRM.Domain.Entity;
using CRM.Domain.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
            using (var transaction = await _context.Database.BeginTransactionAsync())

                try
                {
                    if (entity != null)
                    {
                        await _context.Customers.AddAsync(entity);
                    }

                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    _logger.Log(LogLevel.Error, "Error during adding customer");
                }

            return entity.Id;
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> GetById(int id)
        {
            return await _context.Customers.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
