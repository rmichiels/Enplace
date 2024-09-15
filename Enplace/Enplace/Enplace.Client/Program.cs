using Enplace.Client;
using Enplace.Library.Context;
using Enplace.Service.DTO;
using Enplace.Service.Entities;
using Enplace.Service.Services.API;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Text.Encodings.Web;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
ContextProvider provider = new("https://localhost:7283/api/v1/Configuration");
var context = provider.GetContext();
builder.Services.AddSingleton(context);
builder.Services.AddScoped<ApiService<RecipeDTO>>(serv => new("https://localhost:7283/api/v1/Recipes"));

await builder.Build().RunAsync();
