// Generated Code
namespace XTI_HubAppClient;
public sealed partial class RegisterAppRequest
{
    public XtiVersionModel[] Versions { get; set; } = new XtiVersionModel[0];
    public string Domain { get; set; } = "";
    public AppVersionKey VersionKey { get; set; } = new AppVersionKey();
    public AppApiTemplateModel AppTemplate { get; set; } = new AppApiTemplateModel();
}