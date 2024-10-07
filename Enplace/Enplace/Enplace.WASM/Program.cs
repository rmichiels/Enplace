using Blazored.LocalStorage;
using Blazored.Modal;
using Enplace.Library.Contracts;
using Enplace.Service;
using Enplace.Service.DTO;
using Enplace.Service.Migrations;
using Enplace.Service.Services.API;
using Enplace.WASM;
using Enplace.WASM.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Base services
builder.Services.AddSingleton<ClientConfigurationMap>();
builder.Services.AddBlazoredLocalStorageAsSingleton();
builder.Services.AddBlazoredModal();

// Intermediary Services
builder.Services.AddSingleton<IHttpClientFactory, CustomHttpProvider>();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, SwissknifeAuthStateProvider>();

// Top services
builder.Services.AddSingleton<ConfigurationService>();
builder.Services.AddScoped<ApiService<RecipeDTO>>();
builder.Services.AddScoped<MenuAPI>();
builder.Services.AddScoped<IResource<IngredientDTO>, IngredientResourceService>();
builder.Services.AddScoped<AuthService>();

await builder.Build().RunAsync();
