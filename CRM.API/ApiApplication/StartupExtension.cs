using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ApiApplication
{
    public static class StartupExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(typeof(StartupExtension).GetTypeInfo().Assembly);
        }
    }
}