using XTI_App.Abstractions;
using XTI_App.Secrets;
using XTI_Credentials;
using XTI_Hub.Abstractions;

namespace XTI_Admin;

internal sealed class LocalInstallCommand : ICommand
{
    private readonly Scopes scopes;

    public LocalInstallCommand(Scopes scopes)
    {
        this.scopes = scopes;
    }

    public async Task Execute()
    {
        using var publishedAssets = scopes.GetRequiredService<IPublishedAssets>();
        var options = scopes.GetRequiredService<AdminOptions>();
        var credentials = scopes.GetRequiredService<InstallationUserCredentials>();
        var storedObjFactory = scopes.GetRequiredService<StoredObjectFactory>();
        var storageName = new StorageName("XTI Remote Install");
        var adminInstallOptions = await storedObjFactory.CreateStoredObject(storageName)
            .Value<AdminInstallOptions>(options.RemoteInstallKey);
        var installerCreds = new CredentialValue
        (
            adminInstallOptions.InstallerUserName,
            adminInstallOptions.InstallerPassword
        );
        await credentials.Update(installerCreds);
        await new LocalInstallProcess(scopes, publishedAssets).Run(adminInstallOptions);
    }
}
