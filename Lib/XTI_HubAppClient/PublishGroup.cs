// Generated Code
namespace XTI_HubAppClient;
public sealed partial class PublishGroup : AppClientGroup
{
    public PublishGroup(IHttpClientFactory httpClientFactory, IXtiToken xtiToken, AppClientUrl clientUrl) : base(httpClientFactory, xtiToken, clientUrl, "Publish")
    {
    }

    public Task<AppVersionModel> NewVersion(string modifier, NewVersionRequest model) => Post<AppVersionModel, NewVersionRequest>("NewVersion", modifier, model);
    public Task<AppVersionModel> BeginPublish(string modifier, PublishVersionRequest model) => Post<AppVersionModel, PublishVersionRequest>("BeginPublish", modifier, model);
    public Task<AppVersionModel> EndPublish(string modifier, PublishVersionRequest model) => Post<AppVersionModel, PublishVersionRequest>("EndPublish", modifier, model);
    public Task<AppVersionModel[]> GetVersions(string modifier, AppKey model) => Post<AppVersionModel[], AppKey>("GetVersions", modifier, model);
}