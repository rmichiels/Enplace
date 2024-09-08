using Enplace.Client;
using Enplace.Service.DTO;
using Enplace.Service.Entities;
using Enplace.Service.Services.API;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Text.Encodings.Web;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped<ApiService<RecipeDTO>>(serv => new("https://localhost:7283/api/v1/Recipes"));

await builder.Build().RunAsync();
