using CRM.Domain.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CRM.Infrastructure
{
    public class AppDataContext : IdentityDbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public DbSet<CustomerAddressDetails> AddressDetails { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<CustomerContactInformation> CustomerContactInformations { get; set; }

        public DbSet<CustomerContact> CustomerContacts { get; set; }

        public DbSet<ContactType> ContactTypes { get; set; }

        public DbSet<CustomerStatus> CustomerStatuses { get; set; }

        public AppDataContext(DbContextOptions<AppDataContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Customer>()
                .HasOne(x => x.AddressDetails)
                .WithOne(x => x.Company)
                .HasForeignKey<CustomerAddressDetails>(x => x.CompanyId);

            builder.Entity<Customer>()
                .HasOne(x => x.CEO)
                .WithOne(x => x.Company)
                .HasForeignKey<ApplicationUser>(x => x.CompanyId);

            builder.Entity<Customer>()
                .HasOne(x => x.ContactInformation)
                .WithOne(x => x.Company)
                .HasForeignKey<CustomerContactInformation>(x => x.CompanyId);

            builder.Seed();
        }
    }
}