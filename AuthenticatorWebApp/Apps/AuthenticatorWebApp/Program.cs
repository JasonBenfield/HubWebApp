using XTI_HubAppClient.WebApp.Extensions;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using AuthenticatorWebApp.ApiControllers;
using XTI_Core;
using XTI_AuthenticatorWebAppApi;
using XTI_App.Api;

var builder = XtiWebAppHost.CreateDefault(AuthenticatorInfo.AppKey, args);
var xtiEnv = XtiEnvironment.Parse(builder.Environment.EnvironmentName);
builder.Services.ConfigureXtiCookieAndTokenAuthentication(xtiEnv, builder.Configuration);
builder.Services.AddScoped<AppApiFactory, AuthenticatorAppApiFactory>();
builder.Services.AddScoped(sp => (AuthenticatorAppApi)sp.GetRequiredService<IAppApi>());
builder.Services.AddAuthenticatorAppApiServices();
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