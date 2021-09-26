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
    public class ActivityTypeRepository : BaseRepository, IActivityTypeRepository
    {
        public ActivityTypeRepository(AppDbContext context) : base(context) { }
        public async Task<List<ActivityType>> GetActivityTypesAsync()
        {
            return await _context.ActivityTypes.ToListAsync();
        }
    }
}
