using ApiDomain.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ApiInfrastructure
{
    public class AppDbContext : IdentityDbContext
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<ApplicationClaim> ApplicationClaims { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Seed();
        }
    }
}