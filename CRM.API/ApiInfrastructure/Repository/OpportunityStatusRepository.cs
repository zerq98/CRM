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
    public class OpportunityStatusRepository : BaseRepository, IOpportunityStatusRepository
    {
        public OpportunityStatusRepository(AppDbContext context) : base(context) { }
        public async Task<List<OpportunityStatus>> GetOpportunityStatusesAsync()
        {
            return await _context.OpportunityStatuses.ToListAsync();
        }
    }
}
