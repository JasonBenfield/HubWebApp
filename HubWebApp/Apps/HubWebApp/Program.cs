using HubWebApp.ApiControllers;
using HubWebApp.Extensions;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.OData;
using XTI_App.Hosting;
using XTI_Core;
using XTI_Core.Extensions;
using XTI_Hub;
using XTI_HubWebAppApi;
using XTI_Schedule;
using XTI_Secrets.Extensions;
using XTI_WebApp.Api;
using XTI_WebApp.Extensions;
using XTI_WebApp.Scheduled;

var builder = WebApplication.CreateBuilder(args);
var appKey = HubInfo.AppKey;
builder.Configuration.UseXtiConfiguration(builder.Environment, appKey.Name.DisplayText, appKey.Type.DisplayText, args);
builder.Services.AddResponseCaching();
var xtiEnv = XtiEnvironment.Parse(builder.Environment.EnvironmentName);
builder.Services.ConfigureXtiCookieAndTokenAuthentication(xtiEnv, builder.Configuration);
builder.Services.AddFileSecretCredentials(xtiEnv);
builder.Services.AddServicesForHub();
builder.Services.AddScheduledWebServices((sp, agenda) => { });
builder.Services
    .AddMvc()
    .AddOData(options =>
    {
        var edmModel = new EdmModelBuilder().GetEdmModel();
        options.EnableQueryFeatures(10000)
            .AddRouteComponents("odata", edmModel);
    })
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
app.UseODataQueryRequest();
app.UseXtiDefaults();
await app.RunAsync();