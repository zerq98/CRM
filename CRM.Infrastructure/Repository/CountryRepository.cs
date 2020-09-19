using CRM.Domain.Entity;
using CRM.Domain.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Infrastructure.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly AppDataContext _context;

        public CountryRepository(AppDataContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(Country entity)
        {
            await _context.Countries.AddAsync(entity);

            return entity.Id;
        }

        public async Task<List<Country>> GetAllAsync()
        {
            return await _context.Countries.ToListAsync();
        }

        public async Task<Country> GetById(int id)
        {
            return await _context.Countries.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
