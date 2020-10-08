using AutoMapper;
using CRM.Application.Interface;
using CRM.Application.Mapper;
using CRM.Application.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace CRM.Application
{
    public static class StartupExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}