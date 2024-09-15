using Enplace.Library.Context;
using Enplace.Service.DTO;
using Enplace.Service.Services.API;
using Enplace.WASM;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Diagnostics;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
Debug.WriteLine(new Uri(builder.HostEnvironment.BaseAddress));
ConfigurationService service = new("https://localhost:7283/api/v1/Configuration");
builder.Services.AddSingleton(service);
builder.Services.AddSingleton(await service.GetContextAsync());
builder.Services.AddScoped<ApiService<RecipeDTO>>(serv => new("https://localhost:7283/api/v1/Recipes"));

await builder.Build().RunAsync();
