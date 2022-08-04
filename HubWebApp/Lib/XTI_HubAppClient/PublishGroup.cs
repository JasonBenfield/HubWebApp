// Generated Code
namespace XTI_HubAppClient;
public sealed partial class PublishGroup : AppClientGroup
{
    public PublishGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "Publish")
    {
        Actions = new PublishGroupActions(NewVersion: CreatePostAction<NewVersionRequest, XtiVersionModel>("NewVersion"), BeginPublish: CreatePostAction<PublishVersionRequest, XtiVersionModel>("BeginPublish"), EndPublish: CreatePostAction<PublishVersionRequest, XtiVersionModel>("EndPublish"), GetVersions: CreatePostAction<AppKey, XtiVersionModel[]>("GetVersions"));
    }

    public PublishGroupActions Actions { get; }

    public Task<XtiVersionModel> NewVersion(NewVersionRequest model) => Actions.NewVersion.Post("", model);
    public Task<XtiVersionModel> BeginPublish(PublishVersionRequest model) => Actions.BeginPublish.Post("", model);
    public Task<XtiVersionModel> EndPublish(PublishVersionRequest model) => Actions.EndPublish.Post("", model);
    public Task<XtiVersionModel[]> GetVersions(AppKey model) => Actions.GetVersions.Post("", model);
    public sealed record PublishGroupActions(AppClientPostAction<NewVersionRequest, XtiVersionModel> NewVersion, AppClientPostAction<PublishVersionRequest, XtiVersionModel> BeginPublish, AppClientPostAction<PublishVersionRequest, XtiVersionModel> EndPublish, AppClientPostAction<AppKey, XtiVersionModel[]> GetVersions);
}