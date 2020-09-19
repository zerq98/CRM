using CRM.Domain.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CRM.Infrastructure
{
    public class AppDataContext : IdentityDbContext
    {
        public DbSet<Company> Companies { get; set; }

        public DbSet<CompanyAddressDetails> AddressDetails { get; set; }

        public DbSet<Country> Countries { get; set; }

        public AppDataContext(DbContextOptions<AppDataContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Company>()
                .HasOne(x => x.AddressDetails)
                .WithOne(x => x.Company)
                .HasForeignKey<CompanyAddressDetails>(x => x.CompanyId);

            builder.Entity<Company>()
                .HasOne(x => x.CEO)
                .WithOne(x => x.Company)
                .HasForeignKey<ApplicationUser>(x => x.CompanyId);
        }
    }
}
