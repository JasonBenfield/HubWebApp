using HubWebApp.Extensions;
using XTI_Core.Extensions;
using XTI_Core;
using XTI_WebApp.Extensions;
using XTI_Hub;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using HubWebApp.Controllers;
using XTI_Secrets.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.UseXtiConfiguration(builder.Environment, HubInfo.AppKey.Name.DisplayText, HubInfo.AppKey.Type.DisplayText, args);
builder.Services.AddResponseCaching();
var xtiEnv = XtiEnvironment.Parse(builder.Environment.EnvironmentName);
builder.Services.ConfigureXtiCookieAndTokenAuthentication(xtiEnv, builder.Configuration);
builder.Services.AddFileSecretCredentials(xtiEnv);
builder.Services.AddServicesForHub();
builder.Services
    .AddMvc()
    .AddJsonOptions(options =>
    {
        options.SetDefaultJsonOptions();
    })
    .AddMvcOptions(options =>
    {
        options.SetDefaultMvcOptions();
    });
builder.Services.AddControllersWithViews()
    .PartManager.ApplicationParts.Add
    (
        new AssemblyPart(typeof(HomeController).Assembly)
    );
if (xtiEnv.IsDevelopment())
{
    builder.Services.AddCors
    (
        options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.AllowCredentials();
                builder.SetIsOriginAllowed(origin => new Uri(origin).Host.StartsWith("development.", StringComparison.OrdinalIgnoreCase));
            });
        }
    );
}
var app = builder.Build();
if (xtiEnv.IsDevelopment())
{
    app.UseCors();
}
app.UseXtiDefaults();
await app.RunAsync();