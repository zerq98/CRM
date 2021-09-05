using ApiDomain.Entity;
using ApiDomain.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiInfrastructure.Repository
{
    public class LeadRepository : BaseRepository, ILeadRepository
    {
        public LeadRepository(AppDbContext context) : base(context) { }
        public async Task<Lead> AddLeadAsync(Lead lead)
        {
            using (var transact = _context.Database.BeginTransaction())
            {
                try
                {
                    await _context.LeadContacts.AddRangeAsync(lead.LeadContacts);
                    await _context.LeadAddresses.AddAsync(lead.LeadAddress);
                    await _context.Activities.AddRangeAsync(lead.Activities);
                    await _context.Leads.AddAsync(lead);
                    await _context.SaveChangesAsync();
                    transact.Commit();
                }
                catch(Exception ex)
                {
                    await _context.Logs.AddAsync(new Log
                    {
                        LogMessage = ex.Message,
                        ModuleName = "LeadRepository/AddLeadAsync"
                    });
                    transact.Rollback();

                    throw;
                }
            }
                return lead;
        }

        public async Task<List<Lead>> GetAllLeadsAsync(int companyId)
        {
            return await _context.Leads.Where(x => x.CompanyId == companyId).ToListAsync();
        }
    }
}
