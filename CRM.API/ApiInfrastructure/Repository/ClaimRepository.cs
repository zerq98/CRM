using ApiDomain.Entity;
using ApiDomain.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiInfrastructure.Repository
{
    public class ClaimRepository : BaseRepository, IClaimRepository
    {
        public ClaimRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<string>> GetApplicationClaimsAsync()
        {
            return await _context.ApplicationClaims.Where(x=>x.Id!=1).Select(x=>x.Name).ToListAsync();
        }
    }
}