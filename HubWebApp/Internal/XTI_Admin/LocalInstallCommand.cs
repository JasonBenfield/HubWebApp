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
        var adminTokenAccessor = scopes.GetRequiredService<IAdminTokenAccessor>();
        adminTokenAccessor.UseAnonymousToken();
        var options = scopes.GetRequiredService<AdminOptions>();
        var storedObjFactory = scopes.GetRequiredService<StoredObjectFactory>();
        var storageName = new StorageName("XTI Remote Install");
        var adminInstallOptions = await storedObjFactory.CreateStoredObject(storageName)
            .Value<AdminInstallOptions>(options.RemoteInstallKey);
        adminTokenAccessor.UseInstallerToken();
        options.RepoOwner = adminInstallOptions.RepoOwner;
        options.RepoName = adminInstallOptions.RepoName;
        options.AppName = adminInstallOptions.AppKey.Name.DisplayText;
        options.AppType = adminInstallOptions.AppKey.Type.DisplayText;
        options.VersionKey = adminInstallOptions.VersionKey.DisplayText;
        using var publishedAssets = scopes.GetRequiredService<IPublishedAssets>();
        await new LocalInstallProcess(scopes, publishedAssets).Run(adminInstallOptions);
    }
}
