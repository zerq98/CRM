using AutoMapper;
using CRM.Application;
using CRM.Application.Dto.User;
using CRM.Application.Mapper;
using CRM.Application.Service;
using CRM.Application.Validators;
using CRM.Infrastructure;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CRM.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDataContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("SqlDb")));

            services.AddApplication();
            services.AddInfrastructure();
            services.AddAutoMapper(typeof(Application.StartupExtension));

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Administration",
                    policy => policy.RequireRole("Admin"));
                options.AddPolicy("Overall management",
                    policy => policy.RequireRole("CEO"));
                options.AddPolicy("Service",
                    policy => policy.RequireRole("Service Manager", "Service worker"));
                options.AddPolicy("Marketing",
                    policy => policy.RequireRole("Marketing Manager", "Marketing worker"));
                options.AddPolicy("Sales",
                    policy => policy.RequireRole("Sales Manager", "Sales worker"));
            });

            services.AddMvc(options =>
            {
                //var policy = new AuthorizationPolicyBuilder()
                //                .RequireAuthenticatedUser()
                //                .Build();
                //options.Filters.Add(new AuthorizeFilter(policy));
            }).AddXmlSerializerFormatters().AddFluentValidation(config=>config.RegisterValidatorsFromAssemblyContaining<UserService>());

            services.AddControllersWithViews().AddFluentValidation();

            services.AddHttpContextAccessor();

            services.AddTransient<IValidator<ApplicationUserCreateVM>, UserValidation>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}