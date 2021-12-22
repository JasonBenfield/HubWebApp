// Generated Code
namespace XTI_HubAppClient;
public sealed partial class AppVersionModel
{
    public int ID { get; set; }

    public string VersionKey { get; set; } = "";
    public int Major { get; set; }

    public int Minor { get; set; }

    public int Patch { get; set; }

    public AppVersionType VersionType { get; set; } = AppVersionType.Values.GetDefault();
    public AppVersionStatus Status { get; set; } = AppVersionStatus.Values.GetDefault();
    public DateTimeOffset TimeAdded { get; set; }
}