// Generated Code
namespace XTI_HubAppClient;
public sealed partial class PublishGroup : AppClientGroup
{
    public PublishGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "Publish")
    {
        Actions = new PublishGroupActions(BeginPublish: CreatePostAction<PublishVersionRequest, XtiVersionModel>("BeginPublish"), EndPublish: CreatePostAction<PublishVersionRequest, XtiVersionModel>("EndPublish"), GetVersions: CreatePostAction<AppKeyRequest, XtiVersionModel[]>("GetVersions"), NewVersion: CreatePostAction<NewVersionRequest, XtiVersionModel>("NewVersion"));
    }

    public PublishGroupActions Actions { get; }

    public Task<XtiVersionModel> BeginPublish(PublishVersionRequest requestData, CancellationToken ct = default) => Actions.BeginPublish.Post("", requestData, ct);
    public Task<XtiVersionModel> EndPublish(PublishVersionRequest requestData, CancellationToken ct = default) => Actions.EndPublish.Post("", requestData, ct);
    public Task<XtiVersionModel[]> GetVersions(AppKeyRequest requestData, CancellationToken ct = default) => Actions.GetVersions.Post("", requestData, ct);
    public Task<XtiVersionModel> NewVersion(NewVersionRequest requestData, CancellationToken ct = default) => Actions.NewVersion.Post("", requestData, ct);
    public sealed record PublishGroupActions(AppClientPostAction<PublishVersionRequest, XtiVersionModel> BeginPublish, AppClientPostAction<PublishVersionRequest, XtiVersionModel> EndPublish, AppClientPostAction<AppKeyRequest, XtiVersionModel[]> GetVersions, AppClientPostAction<NewVersionRequest, XtiVersionModel> NewVersion);
}