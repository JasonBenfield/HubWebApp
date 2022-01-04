// Generated Code
namespace XTI_HubAppClient;
public sealed partial class VersionGroup : AppClientGroup
{
    public VersionGroup(IHttpClientFactory httpClientFactory, IXtiToken xtiToken, AppClientUrl clientUrl) : base(httpClientFactory, xtiToken, clientUrl, "Version")
    {
    }

    public Task<AppVersionModel> GetVersion(string modifier, string model) => Post<AppVersionModel, string>("GetVersion", modifier, model);
    public Task<ResourceGroupModel> GetResourceGroup(string modifier, GetVersionResourceGroupRequest model) => Post<ResourceGroupModel, GetVersionResourceGroupRequest>("GetResourceGroup", modifier, model);
}