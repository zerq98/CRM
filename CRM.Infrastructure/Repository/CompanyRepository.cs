using CRM.Domain.Entity;
using CRM.Domain.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

        public CompanyRepository(AppDataContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<int> AddAsync(Company entity)
        {
            if (entity != null)
            {
                await _context.AddressDetails.AddAsync(entity.AddressDetails);
                await _userManager.CreateAsync(entity.CEO, "password");
                await _context.Companies.AddAsync(entity);
            }

            return entity.Id;
        }

        public async Task<string> AddEmployee(int companyId, ApplicationUser user)
        {
            var company = await _context.Companies.SingleOrDefaultAsync(x => x.Id == companyId);

            if (company != null)
            {
                await _userManager.CreateAsync(user, "password");
                company.Employees.Add(user);
                await _context.SaveChangesAsync();
            }

            return user.Id;
        }

        public async Task<List<Company>> GetAllAsync()
        {
            return await _context.Companies.ToListAsync();
        }

        public async Task<List<ApplicationUser>> GetAllEmployeesForCompanyAsync(int companyId)
        {
            return await _userManager.Users.Where(x => x.CompanyId == companyId).ToListAsync();
        }

        public async Task<Company> GetById(int id)
        {
            return await _context.Companies.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ApplicationUser> GetUserById(string userId)
        {
            return await _context.FindAsync<ApplicationUser>(userId);
        }

        public async Task RemoveEmployee(string employeeId)
        {
            var employee = await _context.FindAsync<ApplicationUser>(employeeId);

            if (employee != null)
            {
                _context.Users.Remove(employee);
                await _context.SaveChangesAsync();
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
