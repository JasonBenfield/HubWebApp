using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed class AddVersionRequest
{
    public AddVersionRequest()
        : this(new XtiVersionModel())
    {
    }

    public AddVersionRequest(XtiVersionModel source)
        : this(source.VersionName, source.VersionKey, source.VersionType, source.Status, source.VersionNumber)
    {
    }

    public AddVersionRequest(AppVersionName versionName, AppVersionKey versionKey, AppVersionType versionType, AppVersionStatus status, AppVersionNumber versionNumber)
    {
        VersionName = versionName.DisplayText;
        VersionKey = versionKey.DisplayText;
        VersionType = versionType.Value;
        VersionStatus = status.Value;
        VersionNumber = new AppVersionNumberRequest(versionNumber);
    }

    public string VersionName { get; set; }
    public string VersionKey { get; set; }
    public int VersionType { get; set; }
    public int VersionStatus { get; set; }
    public AppVersionNumberRequest VersionNumber { get; set; }

    public AppVersionName ToAppVersionName() => new AppVersionName(VersionName);

    public AppVersionKey ToAppVersionKey() => AppVersionKey.Parse(VersionKey);

    public AppVersionType ToAppVersionType() => AppVersionType.Values.Value(VersionType);

    public AppVersionStatus ToAppVersionStatus() => AppVersionStatus.Values.Value(VersionStatus);
}
