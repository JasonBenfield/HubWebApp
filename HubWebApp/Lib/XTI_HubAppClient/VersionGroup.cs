// Generated Code
namespace XTI_HubAppClient;
public sealed partial class VersionGroup : AppClientGroup
{
    public VersionGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "Version")
    {
        Actions = new VersionGroupActions(GetVersion: CreatePostAction<string, XtiVersionModel>("GetVersion"));
    }

    public VersionGroupActions Actions { get; }

    public Task<XtiVersionModel> GetVersion(string modifier, string requestData, CancellationToken ct = default) => Actions.GetVersion.Post(modifier, requestData, ct);
    public sealed record VersionGroupActions(AppClientPostAction<string, XtiVersionModel> GetVersion);
}