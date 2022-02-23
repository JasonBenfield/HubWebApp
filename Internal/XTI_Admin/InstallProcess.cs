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
        var appKey = options.AppKey();
        if (!appKey.Type.Equals(AppType.Values.Package))
        {
            var credentials = await addInstallationUser();
            options.InstallationUserName = credentials.UserName;
            options.InstallationPassword = credentials.Password;
            var installMachineName = getMachineName();
            await newInstallation
            (
                appKey,
                installMachineName
            );
            if (string.IsNullOrWhiteSpace(options.DestinationMachine))
            {
                await new LocalInstallProcess(scopes).Run();
            }
            else
            {
                await new LocalInstallServiceProcess(scopes).Run();
            }
        }
    }

    private async Task<CredentialValue> addInstallationUser()
    {
        Console.WriteLine("Adding installation user");
        var machineName = getMachineName();
        var password = $"{Guid.NewGuid():N}?!";
        var hubAdministration = scopes.GetRequiredService<IHubAdministration>();
        var installationUser = await hubAdministration.AddOrUpdateInstallationUser(machineName, password);
        var credentials = new CredentialValue(installationUser.UserName, password);
        Console.WriteLine($"Added installation user '{installationUser.UserName}'");
        return credentials;
    }

    private Task newInstallation(AppKey appKey, string machineName)
    {
        Console.WriteLine("New installation");
        var hubAdministration = scopes.GetRequiredService<IHubAdministration>();
        return hubAdministration.NewInstallation(appKey, machineName);
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