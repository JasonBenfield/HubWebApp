using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XTI_Core;
using XTI_HubAppClient;

namespace HubAppClientTest;

internal sealed class HostedService : IHostedService
{
    private readonly IServiceProvider sp;

    public HostedService(IServiceProvider sp)
    {
        this.sp = sp.CreateScope().ServiceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var xtiEnv = sp.GetRequiredService<XtiEnvironment>();
        Console.WriteLine($"xtiEnv: {xtiEnv.EnvironmentName}");
        try
        {
            var hubClient = sp.GetRequiredService<HubAppClient>();
            var apps = await hubClient.Apps.GetApps();
            var appsText = string.Join(",", apps.Select(a => a.AppKey.Format()));
            Console.WriteLine($"apps: {appsText}");
            var app = await hubClient.App.GetApp("HubWebApp");
            Console.WriteLine($"app: {app.AppKey.Format()}");
            var version = await hubClient.Version.GetVersion("HubWebApp", "Current");
            Console.WriteLine($"verson: {version.VersionKey}");
            var logEntries = await hubClient.LogEntryQuery.Get
            (
                "",
                "$select=AppKey,RequestID,SeverityText,Caption,Message&$top=10",
                new LogEntryQueryRequest
                {
                    RequestID = 1588081
                },
                default
            );
            Console.WriteLine($"logEntries: {XtiSerializer.Serialize(logEntries)}");
            var userAccess = await hubClient.AppUser.GetUserAccess
            (
                "XTI",
                new UserModifierKey
                {
                    UserID = 1009,
                    ModifierID = 17
                }
            );
            Console.WriteLine($"hasAccess: {userAccess.HasAccess}");
            var fileResult = await hubClient.UserQuery.ToExcel
            (
                "", 
                "$select=UserName,Email", 
                new UserGroupKey { UserGroupName = "XTI" }, 
                cancellationToken
            );
            fileResult.WriteToFile(AppDomain.CurrentDomain.BaseDirectory, "users.xlsx", true);

            var fileResult2 = await hubClient.LogEntryQuery.ToExcel
            (
                "",
                "$select=AppKey,RequestID,SeverityText,Caption,Message&$top=10",
                new LogEntryQueryRequest(),
                default
            );
            fileResult2.WriteToFile(AppDomain.CurrentDomain.BaseDirectory, "log_entries.xlsx", true);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex);
        }
        var lifetime = sp.GetRequiredService<IHostApplicationLifetime>();
        lifetime.StopApplication();
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
