using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed class GetVersionRequest
{
    public GetVersionRequest()
        : this(AppVersionName.Unknown, AppVersionKey.None)
    {
    }

    public GetVersionRequest(AppVersionName versionName, AppVersionKey versionKey)
    {
        VersionName = versionName.DisplayText;
        VersionKey = versionKey.DisplayText;
    }

    public string VersionName { get; set; }
    public string VersionKey { get; set; }

    public AppVersionName ToAppVersionName() => new AppVersionName(VersionName);

    public AppVersionKey ToAppVersionKey() => AppVersionKey.Parse(VersionKey);
}