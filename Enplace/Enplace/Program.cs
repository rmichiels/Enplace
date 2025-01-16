using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Enplace.Components;
using Enplace.Service;
using Enplace.Service.Database;
using Enplace.Telemetry;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Text;

var builder = WebApplication.CreateBuilder(args);



Uri keyVaultEndpoint = new(Environment.GetEnvironmentVariable("VaultUri") ?? string.Empty);
Uri cfgEndpoint = new(Environment.GetEnvironmentVariable("CfgUri") ?? string.Empty);

var cred = new DefaultAzureCredential();

var secretClient = new SecretClient(new Uri(keyVaultEndpoint.ToString()), cred);
var sink = await secretClient.GetSecretAsync("k-enplace-api-sink");
Assert.IsNotNull(sink.Value.Value);

var dbcred = await secretClient.GetSecretAsync("k-enplace-db-password");
Assert.IsNotNull(dbcred.Value.Value);

builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
builder.Configuration.AddAzureAppConfiguration(options =>
{
    options.Connect(cfgEndpoint, new DefaultAzureCredential());
    options.ConfigureKeyVault(cfg => cfg.Register(secretClient));
});

builder.Services.Configure<MySqlKeyChain>(builder.Configuration.GetSection("Enplace:MySqlKeyChain"));

// Inject Telemetry
builder.Logging.AddApplicationInsights();
builder.Services.AddApplicationInsightsTelemetry();

builder.Services.AddApplicationInsightsTelemetryProcessor<TelemetryFilterNoAppMetrics>();
builder.Services.AddApplicationInsightsTelemetryProcessor<TelemetryFilterDependecyOk>();

builder.Services.AddCors(
    options => options.AddDefaultPolicy(builder =>
        builder.AllowAnyHeader()
         .WithOrigins(
            "https://sk-enplace-client.azurewebsites.net", 
            "https://localhost:7282", 
            "https://localhost:7287", 
            "https://enplace.swissknife.solutions", 
            "https://enplace.api.swissknife.solutions",
            "https://0.0.0.0"
        )
        .AllowAnyMethod()
    )
);

// Configure Auth
Debug.WriteLine(sink.Value.Value);
Debug.WriteLine(Encoding.UTF8.GetBytes(sink.Value.Value).ToString());
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(sink.Value.Value));

IdentityModelEventSource.ShowPII = true;
IdentityModelEventSource.LogCompleteSecurityArtifact = true;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
  .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
  {
      options.Audience = builder.Configuration["TokenConfig:Audience"];
      options.TokenValidationParameters =
       new TokenValidationParameters
       {
           ValidateIssuer = true,
           ValidIssuer = builder.Configuration["TokenConfig:Issuer"],
           ValidateIssuerSigningKey = true,
           IssuerSigningKey = key,
           ValidateLifetime = true
       };
  });

builder.Services.AddAuthorization(
    options => options.ApplyPolicyDefinitions(builder.Configuration)
);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

var keychainSection = builder.Configuration.GetSection("Enplace:MySqlKeyChain");
var keychain = keychainSection.Get<MySqlKeyChain>();

builder.Services.AddEnplaceServices(keychain);
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(
    options =>
    {
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            BearerFormat = "JWT"
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            }, []
        }
    });
    }
    );
builder.Services.AddApplicationInsightsTelemetry(new Microsoft.ApplicationInsights.AspNetCore.Extensions.ApplicationInsightsServiceOptions
{
    ConnectionString = builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]
});

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
app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Enplace API V1");
    c.RoutePrefix = string.Empty;
});



app.Run();
