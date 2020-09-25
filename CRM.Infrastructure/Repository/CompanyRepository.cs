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
    public class CompanyRepository : ICompanyRepository
    {
        private readonly AppDataContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<CompanyRepository> _logger;

        public CompanyRepository(AppDataContext context,
                                 UserManager<ApplicationUser> userManager,
                                 ILogger<CompanyRepository> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<int> AddAsync(Customer entity)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())

                try
                {
                    if (entity != null)
                    {
                        await _userManager.CreateAsync(entity.CEO, "password");
                        await _context.Companies.AddAsync(entity);
                    }

                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    _logger.Log(LogLevel.Error, "Error during creating company");
                }

            return entity.Id;
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            return await _context.Companies.ToListAsync();
        }

        public async Task<List<ApplicationUser>> GetAllEmployeesForCompanyAsync(int companyId)
        {
            return await _userManager.Users.Where(x => x.CompanyId == companyId).ToListAsync();
        }

        public async Task<Customer> GetById(int id)
        {
            return await _context.Companies.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ApplicationUser> GetUserById(string userId)
        {
            return await _context.FindAsync<ApplicationUser>(userId);
        }

        public async Task RemoveEmployee(string employeeId)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())

                try
                {
                    var employee = await _context.FindAsync<ApplicationUser>(employeeId);

                    if (employee != null)
                    {
                        _context.Users.Remove(employee);
                        await _context.SaveChangesAsync();
                    }

                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    _logger.Log(LogLevel.Error, $"Error during removing the employee with id: {employeeId}");
                }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
