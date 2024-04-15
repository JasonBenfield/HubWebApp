using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed class AddOrUpdateAppsRequest
{
    public AddOrUpdateAppsRequest()
        : this(AppVersionName.None, [])
    {
    }

    public AddOrUpdateAppsRequest(AppVersionName versionName, params AppKey[] appKeys)
    {
        VersionName = versionName.DisplayText;
        AppKeys = appKeys.Select(a => new AppKeyRequest(a)).ToArray();
    }

    public string VersionName { get; set; }
    public AppKeyRequest[] AppKeys { get; set; }

    public AppVersionName ToAppVersionName() => new (VersionName);

    public AppKey[] ToAppKeys() => AppKeys.Select(a => a.ToAppKey()).ToArray();
}
