using Blazored.LocalStorage;
using Blazored.Modal;
using Enplace.Library.Contracts;
using Enplace.Service.Contracts;
using Enplace.Service.Database;
using Enplace.Service.DTO;
using Enplace.Service.Entities;
using Enplace.Service.Services;
using Enplace.Service.Services.API;
using Enplace.Service.Services.Converters;
using Enplace.Service.Services.Managers;
using Microsoft.AspNetCore.Components.Authorization;
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

        public static IServiceCollection AddClientServices(this IServiceCollection services)
        {
            // Event Managers
            services.AddSingleton<AsyncEventManager<RecipeDTO>>();
            services.AddSingleton<AsyncEventManager<Notification>>();
            services.AddSingleton<AsyncEventManager<MenuDTO>>();
            services.AddSingleton<AsyncEventManager<bool>>();

            // Base services
            services.AddSingleton<ClientConfigurationMap>();
            services.AddBlazoredLocalStorage();
            services.AddBlazoredModal();

            // Intermediary Services
            services.AddScoped<IHttpClientFactory, CustomHttpProvider>();
            services.AddAuthorizationCore();
            services.AddScoped<AuthenticationStateProvider, SwissknifeAuthStateProvider>();

            // Top services
            services.AddScoped<ConfigurationService>();
            services.AddScoped<ApiService<RecipeDTO>>();
            services.AddScoped<ApiService<MenuDTO>>();
            services.AddScoped<ApiService<IngredientDTO>>();

            // besides the base implementation
            // MenuAPI has some unique methods
            services.AddScoped<MenuAPI>();
            services.AddScoped<IResource<IngredientDTO>, IngredientResourceService>();
            services.AddScoped<IResourceProvider<RecipeDTO>, RecipeResourceProvider>();
            services.AddScoped<AuthService>();

            return services;
        }
    }
}
