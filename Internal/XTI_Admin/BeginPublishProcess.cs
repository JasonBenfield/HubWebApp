using Microsoft.Extensions.DependencyInjection;
using XTI_App.Abstractions;
using XTI_Hub.Abstractions;

namespace XTI_Admin;

internal sealed class BeginPublishProcess
{
    private readonly Scopes scopes;
    private readonly AppKey appKey;

    public BeginPublishProcess(Scopes scopes, AppKey appKey)
    {
        this.scopes = scopes;
        this.appKey = appKey;
    }

    public async Task<AppVersionModel> Run()
    {
        Console.WriteLine("Begin Publishing");
        var versionKey = new VersionKeyFromCurrentBranch(scopes).Value();
        var hubAdmin = scopes.Production().GetRequiredService<IHubAdministration>();
        var version = await hubAdmin.BeginPublish(appKey, versionKey);
        return version;
    }
}