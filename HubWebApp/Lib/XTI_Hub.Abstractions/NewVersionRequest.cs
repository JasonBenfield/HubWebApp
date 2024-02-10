using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed class NewVersionRequest
{
    public NewVersionRequest()
        :this(AppVersionName.Unknown, AppVersionType.Values.GetDefault())
    {    
    }

    public NewVersionRequest(AppVersionName versionName, AppVersionType versionType)
    {
        VersionName = versionName.DisplayText;
        VersionType = versionType.Value;
    }

    public string VersionName { get; set; }
    public int VersionType { get; set; }

    public AppVersionName ToAppVersionName() => new AppVersionName(VersionName);

    public AppVersionType ToAppVersionType() => AppVersionType.Values.Value(VersionType);
}