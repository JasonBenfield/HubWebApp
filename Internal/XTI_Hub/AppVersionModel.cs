using XTI_App.Abstractions;

namespace XTI_Hub;

public sealed class AppVersionModel
{
    public int ID { get; set; }
    public string VersionKey { get; set; } = "";
    public int Major { get; set; }
    public int Minor { get; set; }
    public int Patch { get; set; }
    public AppVersionType VersionType { get; set; } = AppVersionType.Values.NotSet;
    public AppVersionStatus Status { get; set; } = AppVersionStatus.Values.NotSet;
    public DateTimeOffset TimeAdded { get; set; }

    public Version Version() => new Version(Major, Minor, Patch);
}