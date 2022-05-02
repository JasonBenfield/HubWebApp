using Microsoft.Extensions.DependencyInjection;
using XTI_App.Abstractions;
using XTI_Hub.Abstractions;

namespace XTI_Admin;

internal sealed class CurrentVersion
{
    private readonly IServiceProvider sp;
    private readonly AppVersionName versionName;

    public CurrentVersion(Scopes scopes, AppVersionName versionName)
    {
        sp = scopes.Production();
        this.versionName = versionName;
    }

    public async Task<XtiVersionModel> Value()
    {
        var hubAdmin = sp.GetRequiredService<IHubAdministration>();
        var version = await hubAdmin.Version(versionName, AppVersionKey.Current);
        return version;
    }
}