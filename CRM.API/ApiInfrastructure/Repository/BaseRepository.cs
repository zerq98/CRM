using ApiDomain.Interface;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace ApiInfrastructure.Repository
{
    public class BaseRepository : IBaseRepository
    {
        internal readonly AppDbContext _context;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
        }

        public IDbContextTransaction GetTransaction()
        {
            return _context.Database.BeginTransaction();
        }
    }
}