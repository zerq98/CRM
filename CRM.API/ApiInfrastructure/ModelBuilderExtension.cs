using ApiDomain.Entity;
using Microsoft.EntityFrameworkCore;

namespace ApiInfrastructure
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder builder)
        {
            CreateRelations(builder);
            InitialData(builder);
        }

        private static void CreateRelations(ModelBuilder builder)
        {
            builder.Entity<Company>()
                .HasMany(x => x.Departments)
                .WithOne(x => x.Company)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Department>()
                .HasMany(x => x.Users)
                .WithOne(x => x.Department)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Company>()
                .HasOne<Address>(x => x.Address)
                .WithMany()
                .HasForeignKey(x => x.AddressId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        private static void InitialData(ModelBuilder builder)
        {
            builder.Entity<ApplicationClaim>()
                .HasData(new ApplicationClaim[]
                {
                    new ApplicationClaim
                    {
                        Id=1,
                        Name="AppAdministrator"
                    },
                    new ApplicationClaim
                    {
                        Id=2,
                        Name="CEO"
                    },
                    new ApplicationClaim
                    {
                        Id=3,
                        Name="IT Administrator"
                    }
                });
        }
    }
}