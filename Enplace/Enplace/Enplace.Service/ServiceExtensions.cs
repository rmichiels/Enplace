using Enplace.Service.Contracts;
using Enplace.Service.Database;
using Enplace.Service.Entities;
using Enplace.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Enplace.Service
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddEnplaceServices(this IServiceCollection services)
        {
            services.AddScoped<IModelConverter<Recipe, Recipe>, HollowModelConverter<Recipe>>();
            services.AddScoped<ICrudable, ContextManager>(cman => new([new SSDBContext(), new LiteDBContext()]));
            return services;
        }
    }
}
