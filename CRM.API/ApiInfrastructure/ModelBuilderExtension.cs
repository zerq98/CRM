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
            builder.Entity<SellOpportunityHeader>()
                .HasOne<OpportunityStatus>(x => x.Status)
                .WithMany()
                .HasForeignKey(x => x.StatusId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<SellOpportunityPosition>()
                .HasOne<SellOpportunityHeader>(x => x.OpportunityHeader)
                .WithMany(x => x.Positions)
                .HasForeignKey(x => x.OpportunityHeaderId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<SellOpportunityHeader>()
                .HasOne<Lead>(x => x.Lead)
                .WithMany()
                .HasForeignKey(x => x.LeadId)
                .OnDelete(DeleteBehavior.Restrict);
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
                    },
                    new ApplicationClaim
                    {
                        Id=4,
                        Name="Trader"
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

            builder.Entity<OpportunityStatus>()
                .HasData(new OpportunityStatus[]
                {
                    new OpportunityStatus
                    {
                        Id=1,
                        Name="Nowa"
                    },
                    new OpportunityStatus
                    {
                        Id=2,
                        Name="Modyfikowana"
                    },
                    new OpportunityStatus
                    {
                        Id=3,
                        Name="Anulowana"
                    },
                    new OpportunityStatus
                    {
                        Id=4,
                        Name="Zaakceptowana"
                    },
                    new OpportunityStatus
                    {
                        Id=5,
                        Name="Oferta"
                    }
                });

            builder.Entity<ActivityType>()
                .HasData(new ActivityType[]
                {
                    new ActivityType
                    {
                        Id=1,
                        Name="Inna aktywność"
                    },
                    new ActivityType
                    {
                        Id=2,
                        Name="Rozmowa telefoniczna"
                    },
                    new ActivityType
                    {
                        Id=3,
                        Name="Wiadmość Email"
                    },
                    new ActivityType
                    {
                        Id=4,
                        Name="Rozmowa online"
                    },
                    new ActivityType
                    {
                        Id=5,
                        Name="Spotkanie z klientem"
                    },
                    new ActivityType
                    {
                        Id=6,
                        Name="Wysłanie oferty"
                    }
                });
        }
    }
}