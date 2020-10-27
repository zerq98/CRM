using AutoMapper;
using CRM.Application.Dto.User;
using CRM.Application.Interface;
using CRM.Application.Mapper;
using CRM.Application.Service;
using CRM.Application.Validators;
using FluentValidation;
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
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IValidator<ApplicationUserCreateVM>, UserValidation>();
        }
    }
}