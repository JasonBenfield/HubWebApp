using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed class AddOrUpdateAppsRequest
{
    public AddOrUpdateAppsRequest()
        : this(AppVersionName.None, "", "", [])
    {
    }

    public AddOrUpdateAppsRequest(AppVersionName versionName, string repoOwner, string repoName, params AppKey[] appKeys)
    {
        VersionName = versionName.DisplayText;
        RepoOwner = repoOwner;
        RepoName = repoName;
        AppKeys = appKeys.Select(a => new AppKeyRequest(a)).ToArray();
    }

    public string VersionName { get; set; }
    public string RepoOwner { get; set; }
    public string RepoName { get; set; }
    public AppKeyRequest[] AppKeys { get; set; }

    public AppVersionName ToAppVersionName() => new (VersionName);

    public AppKey[] ToAppKeys() => AppKeys.Select(a => a.ToAppKey()).ToArray();
}
