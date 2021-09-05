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

            builder.Entity<Activity>()
                .HasOne<ActivityType>(x => x.ActivityType)
                .WithMany()
                .HasForeignKey(x => x.ActivityTypeId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Lead>()
                .HasOne<LeadAddress>(x => x.LeadAddress)
                .WithMany()
                .HasForeignKey(x=>x.LeadAddressId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Lead>()
                .HasOne<LeadStatus>(x => x.LeadStatus)
                .WithMany()
                .HasForeignKey(x => x.LeadStatusId)
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

            builder.Entity<LeadStatus>()
                .HasData(new LeadStatus[]
                {
                    new LeadStatus
                    {
                        Id=1,
                        Name="Nowy"
                    },
                    new LeadStatus
                    {
                        Id=2,
                        Name="Oferta"
                    },
                    new LeadStatus
                    {
                        Id=3,
                        Name="Stracony"
                    },
                    new LeadStatus
                    {
                        Id=4,
                        Name="Odłożony"
                    },
                    new LeadStatus
                    {
                        Id=5,
                        Name="Wygrany"
                    },
                    new LeadStatus
                    {
                        Id=6,
                        Name="Kontrahent"
                    }
                });

            builder.Entity<ActivityType>()
                .HasData(new ActivityType[]
                {
                    new ActivityType
                    {
                        Id=1,
                        Name="Rozmowa telefoniczna"
                    },
                    new ActivityType
                    {
                        Id=2,
                        Name="Wiadmość Email"
                    },
                    new ActivityType
                    {
                        Id=3,
                        Name="Rozmowa online"
                    },
                    new ActivityType
                    {
                        Id=4,
                        Name="Spotkanie z klientem"
                    },
                    new ActivityType
                    {
                        Id=5,
                        Name="Wysłanie oferty"
                    }
                });
        }
    }
}