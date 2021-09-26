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
    public class LeadStatusRepository : BaseRepository, ILeadStatusRepository
    {
        public LeadStatusRepository(AppDbContext context) : base(context) { }
        public async Task<List<LeadStatus>> GetLeadStatusesAsync()
        {
            return await _context.LeadStatuses.ToListAsync();
        }
    }
}
