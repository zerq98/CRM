using ApiApplication.Product;
using ApiApplication.TodoTasks;
using ApiApplication.Validators;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ApiApplication
{
    public static class StartupExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ITodoTaskService, TodoTaskService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddMediatR(typeof(StartupExtension).GetTypeInfo().Assembly);
            services.AddAutoMapper(typeof(StartupExtension));
        }
    }
}