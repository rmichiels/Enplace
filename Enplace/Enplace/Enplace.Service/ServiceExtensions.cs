using Enplace.Service.Contracts;
using Enplace.Service.Database;
using Enplace.Service.DTO;
using Enplace.Service.Entities;
using Enplace.Service.Services.Converters;
using Enplace.Service.Services.Managers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Enplace.Service
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddEnplaceServices(this IServiceCollection services, IDbKeyChain dbKey)
        {
            var msqlOptsBuilder = new DbContextOptionsBuilder<SSDBContext>();
            msqlOptsBuilder.UseMySql(dbKey.GetConnectionString(), ServerVersion.AutoDetect(dbKey.GetConnectionString()));
            services.AddScoped<ICrudable, ContextManager>(cman => new([
                new SSDBContext(msqlOptsBuilder.Options),
                new LiteDBContext()
                ])
            );
            services.AddScoped<SynchronisationManager>(syncman => new(new(), new()));

            services.AddScoped<IModelConverter<UserMenu, MenuDTO>, MenuConverter>();
            services.AddScoped<IModelConverter<Ingredient, IngredientDTO>, IngredientConverter>();
            return services;
        }
    }
}
