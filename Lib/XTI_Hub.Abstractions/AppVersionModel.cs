using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed class AppVersionModel
{
    public int ID { get; set; }
    public string VersionKey { get; set; } = "";
    public AppVersionNumber VersionNumber { get; set; } = new AppVersionNumber(0, 0, 0);
    public AppVersionType VersionType { get; set; } = AppVersionType.Values.NotSet;
    public AppVersionStatus Status { get; set; } = AppVersionStatus.Values.NotSet;
    public DateTimeOffset TimeAdded { get; set; }

    public Version Version() => VersionNumber.ToVersion();

    public Version NextPatch() => VersionNumber.NextPatch().ToVersion();
}