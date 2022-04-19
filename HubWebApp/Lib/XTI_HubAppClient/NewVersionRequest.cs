// Generated Code
namespace XTI_HubAppClient;
public sealed partial class NewVersionRequest
{
    public string GroupName { get; set; } = "";
    public AppVersionType VersionType { get; set; } = AppVersionType.Values.GetDefault();
    public AppDefinitionModel[] AppDefinitions { get; set; } = new AppDefinitionModel[0];
}