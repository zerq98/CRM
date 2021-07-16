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
    public class ClaimRepository : BaseRepository, IClaimRepository
    {
        public ClaimRepository(AppDbContext context): base(context) { }
        public async Task<List<ApplicationClaim>> GetApplicationClaimsAsync()
        {
            return await _context.ApplicationClaims.ToListAsync();
        }
    }
}
