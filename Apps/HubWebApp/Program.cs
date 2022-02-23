using HubWebApp.Extensions;
using XTI_Core.Extensions;
using XTI_Core;
using XTI_WebApp.Extensions;
using XTI_Hub;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.UseXtiConfiguration(builder.Environment, HubInfo.AppKey.Name.DisplayText, HubInfo.AppKey.Type.DisplayText, args);
builder.Services.AddResponseCaching();
var xtiEnv = XtiEnvironment.Parse(builder.Environment.EnvironmentName);
builder.Services.ConfigureXtiCookieAndTokenAuthentication(xtiEnv, builder.Configuration);
builder.Services.AddServicesForHub(builder.Configuration, args);

var app = builder.Build();
app.UseXtiDefaults();
await app.RunAsync();