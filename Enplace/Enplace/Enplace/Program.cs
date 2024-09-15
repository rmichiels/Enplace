using Enplace;
using Enplace.Components;
using Enplace.Library.Context;
using Enplace.Service;
using Enplace.Service.DTO;
using Enplace.Service.Services.API;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(
    options => options.AddDefaultPolicy(builder =>
        builder.AllowAnyHeader()
        .AllowAnyOrigin()
        .AllowAnyMethod()
    )
);
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();
builder.Services.AddEnplaceServices();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<EnplaceContext>();
builder.Services.AddScoped<ApiService<RecipeDTO>>(serv => new("https://localhost:7283/api/v1/Recipes"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors();
app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Enplace.Client._Imports).Assembly);
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blazor API V1");
});


app.Run();
