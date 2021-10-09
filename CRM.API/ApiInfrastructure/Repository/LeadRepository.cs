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

        public async Task<bool> CheckIfNIPExistsAsync(string nip, int leadId, int companyId)
        {
            if (leadId == 0)
            {
                return await _context.Leads.FirstOrDefaultAsync(x => x.NIP == nip && x.CompanyId==companyId) != null;
            }
            else
            {
                return await _context.Leads.FirstOrDefaultAsync(x => x.NIP == nip && x.Id!=leadId && x.CompanyId == companyId) != null;
            }
        }

        public async Task<bool> CheckIfRegonExistsAsync(string regon, int leadId,int companyId)
        {
            if (leadId == 0)
            {
                return await _context.Leads.FirstOrDefaultAsync(x => x.Regon == regon && x.CompanyId == companyId) != null;
            }
            else
            {
                return await _context.Leads.FirstOrDefaultAsync(x => x.Regon == regon && x.Id != leadId && x.CompanyId == companyId) != null;
            }
        }

        public async Task<List<Lead>> GetAllLeadsAsync(int companyId,DateTime DateFrom,DateTime DateTo)
        {
            return await _context.Leads
                .Include(x=>x.LeadContacts)
                .Include(x=> x.LeadStatus)
                .Include(x => x.User)
                .Include(x=>x.Activities)
                .ThenInclude(x=>x.ActivityType)
                .Where(x => x.CompanyId == companyId && x.CreateDate.Date>=DateFrom.Date && x.CreateDate.Date<=DateTo.Date)
                .ToListAsync();
        }

        public async Task<Lead> GetLeadAsync(int leadId,int companyId)
        {
            return await _context.Leads
                .Include(x => x.LeadAddress)
                .Include(x => x.LeadContacts)
                .Include(x => x.LeadStatus)
                .Include(x => x.Activities)
                .ThenInclude(x => x.ActivityType)
                .Include(x => x.Activities)
                .ThenInclude(x => x.User)
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == leadId && x.CompanyId==companyId);
        }

        public async Task<Lead> GetLeadByNameAsync(string name, int companyId)
        {
            var leadName = name.Split(',')[0];
            var nip = name.Split(',')[1];

            return await _context.Leads.FirstOrDefaultAsync(x => x.Name == leadName && x.NIP == nip && x.CompanyId == companyId);
        }

        public async Task<List<int>> GetUserActivitiesCountAsync(string userId)
        {
            var list = new List<int>();

            list.Add((await _context.Activities.Where(x => x.ActivityTypeId == 1 && x.UserId == userId).ToListAsync()).Count);
            list.Add((await _context.Activities.Where(x => x.ActivityTypeId == 2 && x.UserId == userId).ToListAsync()).Count);
            list.Add((await _context.Activities.Where(x => x.ActivityTypeId == 3 && x.UserId == userId).ToListAsync()).Count);
            list.Add((await _context.Activities.Where(x => x.ActivityTypeId == 4 && x.UserId == userId).ToListAsync()).Count);
            list.Add((await _context.Activities.Where(x => x.ActivityTypeId == 5 && x.UserId == userId).ToListAsync()).Count);
            list.Add((await _context.Activities.Where(x => x.ActivityTypeId == 6 && x.UserId == userId).ToListAsync()).Count);

            return list;
        }

        public async Task RemoveActivityAsync(int activityId)
        {
            var activity = await _context.Activities.FirstOrDefaultAsync(x => x.Id == activityId);
            if (activity != null)
            {
                _context.Activities.Remove(activity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveLeadContactAsync(int contactId)
        {
            var contact = await _context.LeadContacts.FirstOrDefaultAsync(x => x.Id == contactId);
            if (contact != null)
            {
                _context.LeadContacts.Remove(contact);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Lead> UpdateAsync(Lead lead)
        {
            using (var transact = _context.Database.BeginTransaction())
            {
                try
                {
                    foreach(var contact in lead.LeadContacts)
                    {
                        if (contact.Id == 0)
                        {
                            await _context.LeadContacts.AddAsync(contact);
                        }
                        else
                        {
                            _context.LeadContacts.Update(contact);
                        }
                    }
                    _context.LeadAddresses.Update(lead.LeadAddress);
                    foreach(var activity in lead.Activities)
                    {
                        if (activity.Id == 0)
                        {
                            await _context.Activities.AddAsync(activity);
                        }
                        else
                        {
                            _context.Activities.Update(activity);
                        }
                    }
                    _context.Leads.Update(lead);
                    await _context.SaveChangesAsync();
                    transact.Commit();
                }
                catch (Exception ex)
                {
                    await _context.Logs.AddAsync(new Log
                    {
                        LogMessage = ex.Message,
                        ModuleName = "LeadRepository/UpdateAsync"
                    });
                    transact.Rollback();

                    throw;
                }
            }
            return lead;
        }
    }
}
