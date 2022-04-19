using XTI_App.Abstractions;
using XTI_Credentials;
using XTI_Hub.Abstractions;

namespace XTI_Admin;

internal sealed class InstallProcess
{
    private readonly Scopes scopes;

    public InstallProcess(Scopes scopes)
    {
        this.scopes = scopes;
    }

    public async Task Run()
    {
        var options = scopes.GetRequiredService<AdminOptions>();
        var selectedAppKeys = scopes.GetRequiredService<SelectedAppKeys>();
        var appKeys = selectedAppKeys.Values.Where(a => !a.Type.Equals(AppType.Values.Package));
        foreach (var appKey in appKeys)
        {
            var installMachineName = getMachineName();
            await newInstallation
            (
                appKey,
                installMachineName
            );
            if (string.IsNullOrWhiteSpace(options.DestinationMachine))
            {
                await new LocalInstallProcess(scopes, appKey).Run();
            }
            else
            {
                await new LocalInstallServiceProcess(scopes, appKey).Run();
            }
        }
    }

    private Task newInstallation(AppKey appKey, string machineName)
    {
        Console.WriteLine("New installation");
        var versionName = new AppVersionName().Value;
        var hubAdministration = scopes.GetRequiredService<IHubAdministration>();
        return hubAdministration.NewInstallation(versionName, appKey, machineName);
    }

    private string getMachineName()
    {
        var options = scopes.GetRequiredService<AdminOptions>();
        string machineName;
        if (string.IsNullOrWhiteSpace(options.DestinationMachine))
        {
            machineName = Environment.MachineName;
        }
        else
        {
            machineName = options.DestinationMachine;
        }
        return machineName;
    }
}