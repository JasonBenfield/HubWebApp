using XTI_App.Abstractions;
using XTI_Hub.Abstractions;

namespace XTI_Admin;

public sealed class CurrentVersion
{
    private readonly IHubService hubAdmin;
    private readonly AppVersionNameAccessor versionNameAccessor;

    public CurrentVersion(ProductionHubService hubAdmin, AppVersionNameAccessor versionNameAccessor)
    {
        this.hubAdmin = hubAdmin.Value;
        this.versionNameAccessor = versionNameAccessor;
    }

    public async Task<XtiVersionModel> Value(CancellationToken ct)
    {
        var versionName = versionNameAccessor.Value;
        var version = await hubAdmin.Version(versionName, AppVersionKey.Current, ct);
        return version;
    }
}