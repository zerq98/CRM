using ApiDomain.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ApiInfrastructure
{
    public class AppDbContext : IdentityDbContext
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<ApplicationClaim> ApplicationClaims { get; set; }
        public DbSet<TodoTask> TodoTasks { get; set; }
        public DbSet<LeadStatus> LeadStatuses { get; set; }
        public DbSet<LeadContact> LeadContacts { get; set; }
        public DbSet<LeadAddress> LeadAddresses { get; set; }
        public DbSet<Lead> Leads { get; set; }
        public DbSet<ActivityType> ActivityTypes { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<OpportunityStatus> OpportunityStatuses { get; set; }
        public DbSet<SellOpportunityHeader> SellOpportunityHeaders { get; set; }
        public DbSet<SellOpportunityPosition> SellOpportunityPositions { get; set; }
        public DbSet<Product> Products { get; set; }

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