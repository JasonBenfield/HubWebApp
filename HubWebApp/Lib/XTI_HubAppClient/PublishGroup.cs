// Generated Code
namespace XTI_HubAppClient;
public sealed partial class PublishGroup : AppClientGroup
{
    public PublishGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl) : base(httpClientFactory, xtiTokenAccessor, clientUrl, "Publish")
    {
    }

    public Task<XtiVersionModel> NewVersion(NewVersionRequest model) => Post<XtiVersionModel, NewVersionRequest>("NewVersion", "", model);
    public Task<XtiVersionModel> BeginPublish(PublishVersionRequest model) => Post<XtiVersionModel, PublishVersionRequest>("BeginPublish", "", model);
    public Task<XtiVersionModel> EndPublish(PublishVersionRequest model) => Post<XtiVersionModel, PublishVersionRequest>("EndPublish", "", model);
    public Task<XtiVersionModel[]> GetVersions(AppKey model) => Post<XtiVersionModel[], AppKey>("GetVersions", "", model);
}