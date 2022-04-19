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
var app = builder.Build();
app.UseXtiDefaults();
await app.RunAsync();