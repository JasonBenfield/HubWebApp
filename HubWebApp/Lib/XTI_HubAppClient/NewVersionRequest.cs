// Generated Code
namespace XTI_HubAppClient;
public sealed partial class NewVersionRequest
{
    public AppVersionName VersionName { get; set; } = new AppVersionName();
    public AppVersionType VersionType { get; set; } = AppVersionType.Values.GetDefault();
}