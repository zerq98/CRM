using CRM.Domain.Entity;
using CRM.Domain.Interface;
using CRM.Infrastructure.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace CRM.Infrastructure
{
    public static class StartupExtension
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddIdentityCore<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDataContext>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<ICountryRepository, CountryRepository>();
            services.AddLogging();
        }
    }
}