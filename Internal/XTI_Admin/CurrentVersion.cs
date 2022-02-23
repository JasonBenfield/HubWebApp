using Microsoft.Extensions.DependencyInjection;
using XTI_App.Abstractions;
using XTI_Hub.Abstractions;

namespace XTI_Admin;

internal sealed class CurrentVersion
{
    private readonly IServiceProvider sp;
    private readonly AppKey appKey;

    public CurrentVersion(Scopes scopes, AppKey appKey)
    {
        sp = scopes.Production();
        this.appKey = appKey;
    }

    public async Task<AppVersionModel> Value()
    {
        var hubAdmin = sp.GetRequiredService<IHubAdministration>();
        var version = await hubAdmin.Version(appKey, AppVersionKey.Current);
        return version;
    }
}