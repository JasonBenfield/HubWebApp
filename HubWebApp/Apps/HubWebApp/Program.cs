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
using XTI_WebApp.Extensions;
using XTI_WebApp.Scheduled;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.UseXtiConfiguration(builder.Environment, HubInfo.AppKey.Name.DisplayText, HubInfo.AppKey.Type.DisplayText, args);
builder.Services.AddResponseCaching();
var xtiEnv = XtiEnvironment.Parse(builder.Environment.EnvironmentName);
builder.Services.ConfigureXtiCookieAndTokenAuthentication(xtiEnv, builder.Configuration);
builder.Services.AddFileSecretCredentials(xtiEnv);
builder.Services.AddServicesForHub();
builder.Services.AddScheduledWebServices
(
    (sp, agenda) =>
    {
        agenda.AddScheduled<HubAppApi>
        (
            (api, agendaItem) =>
            {
                agendaItem.Action(api.Periodic.DeleteExpiredStoredObjects)
                    .Interval(TimeSpan.FromHours(1))
                    .AddSchedule
                    (
                        Schedule.EveryDay().At(TimeRange.AllDay())
                    );
            }
        );
        agenda.AddScheduled<HubAppApi>
        (
            (api, agendaItem) =>
            {
                agendaItem.Action(api.Periodic.EndExpiredSessions)
                    .Interval(TimeSpan.FromHours(1))
                    .AddSchedule
                    (
                        Schedule.EveryDay().At(TimeRange.AllDay())
                    );
            }
        );
        agenda.AddScheduled<HubAppApi>
        (
            (api, agendaItem) =>
            {
                agendaItem.Action(api.Periodic.PurgeLogs)
                    .Interval(TimeSpan.FromHours(7))
                    .AddSchedule
                    (
                        Schedule.EveryDay().At(TimeRange.AllDay())
                    );
            }
        );
    }
);
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