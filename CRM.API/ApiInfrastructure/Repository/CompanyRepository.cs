using ApiDomain.Entity;
using ApiDomain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiInfrastructure.Repository
{
    public class CompanyRepository : BaseRepository, ICompanyRepository
    {
        public CompanyRepository(AppDbContext context) : base(context) { }
        public async Task<Company> CreateCompanyAsync(Company company)
        {
            try
            {
                await _context.Companies.AddAsync(company);
                await _context.SaveChangesAsync();
                return company;
            }
            catch(Exception ex)
            {
                await _context.Logs.AddAsync(new Log
                {
                    LogMessage = ex.Message,
                    ModuleName = "CompanyRepository/CreateCompanyAsync"
                });
                await _context.SaveChangesAsync();

                return null;
            }
        }
    }
}
