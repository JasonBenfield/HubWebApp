// Generated Code
namespace XTI_HubAppClient;
public sealed partial class ExternalAuthGroup : AppClientGroup
{
    public ExternalAuthGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "ExternalAuth")
    {
        Actions = new ExternalAuthGroupActions(ExternalAuthKey: CreatePostAction<ExternalAuthKeyModel, AuthenticatedLoginResult>("ExternalAuthKey"));
    }

    public ExternalAuthGroupActions Actions { get; }

    public Task<AuthenticatedLoginResult> ExternalAuthKey(ExternalAuthKeyModel model, CancellationToken ct = default) => Actions.ExternalAuthKey.Post("", model, ct);
    public sealed record ExternalAuthGroupActions(AppClientPostAction<ExternalAuthKeyModel, AuthenticatedLoginResult> ExternalAuthKey);
}