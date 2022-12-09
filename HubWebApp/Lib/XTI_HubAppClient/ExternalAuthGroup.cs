// Generated Code
namespace XTI_HubAppClient;
public sealed partial class ExternalAuthGroup : AppClientGroup
{
    public ExternalAuthGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "ExternalAuth")
    {
        Actions = new ExternalAuthGroupActions(ExternalAuthKey: CreatePostAction<ExternalAuthKeyModel, string>("ExternalAuthKey"));
    }

    public ExternalAuthGroupActions Actions { get; }

    public Task<string> ExternalAuthKey(ExternalAuthKeyModel model, CancellationToken ct = default) => Actions.ExternalAuthKey.Post("", model, ct);
    public sealed record ExternalAuthGroupActions(AppClientPostAction<ExternalAuthKeyModel, string> ExternalAuthKey);
}