using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
            var fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "userQuery.xlsx");
            File.WriteAllBytes(fileName, fileResult.Content);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
