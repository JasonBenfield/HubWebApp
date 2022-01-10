// Generated Code
namespace XTI_HubAppClient;
public sealed partial class VersionGroup : AppClientGroup
{
    public VersionGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl) : base(httpClientFactory, xtiTokenAccessor, clientUrl, "Version")
    {
    }

    public Task<AppVersionModel> GetVersion(string modifier, string model) => Post<AppVersionModel, string>("GetVersion", modifier, model);
    public Task<ResourceGroupModel> GetResourceGroup(string modifier, GetVersionResourceGroupRequest model) => Post<ResourceGroupModel, GetVersionResourceGroupRequest>("GetResourceGroup", modifier, model);
}