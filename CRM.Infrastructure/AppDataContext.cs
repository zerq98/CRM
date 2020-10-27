using CRM.Domain.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

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
                .WithOne(x => x.Customer)
                .HasForeignKey<CustomerAddressDetails>(x => x.CustomerId);

            builder.Entity<Customer>()
                .HasOne(x => x.ContactInformation)
                .WithOne(x => x.Customer)
                .HasForeignKey<CustomerContactInformation>(x => x.CustomerId);

            builder.Seed();

            const string ADMIN_ID = "a18be9c0-aa65-4af8-bd17-00bd9344e575";
            const string ROLE_ID = ADMIN_ID;

            var hasher = new PasswordHasher<ApplicationUser>();
            builder.Entity<ApplicationUser>()
                .HasData(new ApplicationUser
                {
                    Id = ADMIN_ID,
                    UserName = "admin@arctech.com",
                    NormalizedUserName = "admin@arctech.com",
                    Email = "admin@arctech.com",
                    NormalizedEmail = "admin@arctech.com",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "zaq1@WSX21"),
                    SecurityStamp = string.Empty,
                    FirstName = "Mateusz",
                    LastName = "Trybuła",
                    IsActive = true
                });

            builder.Entity<IdentityRole>()
                .HasData(new IdentityRole
                {
                    Id = ROLE_ID,
                    Name = "Admin",
                    NormalizedName = "Admin"
                });

            builder.Entity<IdentityUserRole<string>>()
                .HasData(new IdentityUserRole<string>
                {
                    RoleId = ROLE_ID,
                    UserId = ADMIN_ID
                });
        }
    }
}