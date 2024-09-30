using Enplace.Service.Contracts;
using Enplace.Service.Database;
using Enplace.Service.Services.Managers;
using Microsoft.Extensions.DependencyInjection;

namespace Enplace.Service
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddEnplaceServices(this IServiceCollection services)
        {
            services.AddScoped<ICrudable, ContextManager>(cman => new([new SSDBContext(), new LiteDBContext()]));
            services.AddScoped<SynchronisationManager>(syncman => new(new(), new()));
            return services;
        }
    }
}
