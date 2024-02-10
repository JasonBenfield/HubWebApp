using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed class GetVersionsRequest
{
    public GetVersionsRequest()
        : this(AppVersionName.Unknown)
    {
    }

    public GetVersionsRequest(AppVersionName versionName)
    {
        VersionName = versionName.DisplayText;
    }

    public string VersionName { get; set; }

    public AppVersionName ToAppVersionName() => new(VersionName);
}
