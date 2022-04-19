using Microsoft.Extensions.DependencyInjection;
using XTI_App.Abstractions;
using XTI_Hub.Abstractions;

namespace XTI_Admin;

internal sealed class CurrentVersion
{
    private readonly IServiceProvider sp;

    public CurrentVersion(Scopes scopes)
    {
        sp = scopes.Production();
    }

    public async Task<XtiVersionModel> Value()
    {
        var hubAdmin = sp.GetRequiredService<IHubAdministration>();
        var version = await hubAdmin.Version(new AppVersionName().Value, AppVersionKey.Current);
        return version;
    }
}