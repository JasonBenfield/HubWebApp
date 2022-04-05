// Generated Code
namespace XTI_HubAppClient;
public sealed partial class PublishGroup : AppClientGroup
{
    public PublishGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl) : base(httpClientFactory, xtiTokenAccessor, clientUrl, "Publish")
    {
    }

    public Task<XtiVersionModel> NewVersion(string modifier, NewVersionRequest model) => Post<XtiVersionModel, NewVersionRequest>("NewVersion", modifier, model);
    public Task<XtiVersionModel> BeginPublish(string modifier, PublishVersionRequest model) => Post<XtiVersionModel, PublishVersionRequest>("BeginPublish", modifier, model);
    public Task<XtiVersionModel> EndPublish(string modifier, PublishVersionRequest model) => Post<XtiVersionModel, PublishVersionRequest>("EndPublish", modifier, model);
    public Task<XtiVersionModel[]> GetVersions(string modifier, AppKey model) => Post<XtiVersionModel[], AppKey>("GetVersions", modifier, model);
}