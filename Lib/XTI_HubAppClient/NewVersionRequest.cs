// Generated Code
namespace XTI_HubAppClient;
public sealed partial class NewVersionRequest
{
    public AppKey AppKey { get; set; } = new AppKey();
    public string Domain { get; set; } = "";
    public AppVersionType VersionType { get; set; } = AppVersionType.Values.GetDefault();
}