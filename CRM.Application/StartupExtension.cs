using AutoMapper;
using CRM.Application.Interface;
using CRM.Application.Mapper;
using CRM.Application.Service;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CRM.Application
{
    public static class StartupExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>(),
                                        AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}