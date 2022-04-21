using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed class XtiVersionModel
{
    public int ID { get; set; }
    public AppVersionName VersionName { get; set; } = AppVersionName.Unknown;
    public AppVersionKey VersionKey { get; set; } = AppVersionKey.None;
    public AppVersionNumber VersionNumber { get; set; } = new AppVersionNumber(0, 0, 0);
    public AppVersionType VersionType { get; set; } = AppVersionType.Values.NotSet;
    public AppVersionStatus Status { get; set; } = AppVersionStatus.Values.NotSet;
    public DateTimeOffset TimeAdded { get; set; }

    public AppVersionNumber NextPatch() => VersionNumber.NextPatch();
}