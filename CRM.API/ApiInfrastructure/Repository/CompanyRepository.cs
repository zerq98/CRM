using ApiDomain.Entity;
using ApiDomain.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiInfrastructure.Repository
{
    public class CompanyRepository : BaseRepository, ICompanyRepository
    {
        public CompanyRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Company> CreateCompanyAsync(Company company)
        {
            try
            {
                await _context.Companies.AddAsync(company);
                await _context.SaveChangesAsync();
                return company;
            }
            catch (Exception ex)
            {
                await _context.Logs.AddAsync(new Log
                {
                    LogMessage = ex.Message,
                    ModuleName = "CompanyRepository/CreateCompanyAsync"
                });
                await _context.SaveChangesAsync();

                throw;
            }
        }

        public async Task<Company> GetByIdAsync(int companyId)
        {
            return await _context.Companies.Include(x=>x.Address).FirstOrDefaultAsync(x => x.Id == companyId);
        }

        public async Task<Company> GetByNameAsync(string name)
        {
            return await _context.Companies.FirstOrDefaultAsync(x => x.CompanyName == name);
        }

        public async Task UpdateCompanyAsync(Company company)
        {
            try
            {
                _context.Addresses.Update(company.Address);
                _context.Companies.Update(company);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _context.Logs.AddAsync(new Log
                {
                    LogMessage = ex.Message,
                    ModuleName = "CompanyRepository/UpdateCompanyAsync"
                });
                await _context.SaveChangesAsync();

                throw;
            }
        }
    }
}