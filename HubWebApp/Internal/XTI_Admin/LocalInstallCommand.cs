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
        var options = scopes.GetRequiredService<AdminOptions>();
        var storedObjFactory = scopes.GetRequiredService<StoredObjectFactory>();
        var storageName = new StorageName("XTI Remote Install");
        var adminInstallOptions = await storedObjFactory.CreateStoredObject(storageName)
            .Value<AdminInstallOptions>(options.RemoteInstallKey);
        options.RepoOwner = adminInstallOptions.RepoOwner;
        options.RepoName = adminInstallOptions.RepoName;
        options.AppName = adminInstallOptions.AppKey.Name.DisplayText;
        options.AppType = adminInstallOptions.AppKey.Type.DisplayText;
        options.VersionKey = adminInstallOptions.VersionKey.DisplayText;
        var installerCreds = new CredentialValue
        (
            adminInstallOptions.InstallerUserName,
            adminInstallOptions.InstallerPassword
        );
        var credentials = scopes.GetRequiredService<InstallationUserCredentials>();
        await credentials.Update(installerCreds);
        using var publishedAssets = scopes.GetRequiredService<IPublishedAssets>();
        await new LocalInstallProcess(scopes, publishedAssets).Run(adminInstallOptions);
    }
}
