// Generated Code
namespace XTI_HubAppClient;
public sealed partial class PeriodicGroup : AppClientGroup
{
    public PeriodicGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "Periodic")
    {
        Actions = new PeriodicGroupActions(PurgeLogs: CreatePostAction<EmptyRequest, EmptyActionResult>("PurgeLogs"));
    }

    public PeriodicGroupActions Actions { get; }

    public Task<EmptyActionResult> PurgeLogs(CancellationToken ct = default) => Actions.PurgeLogs.Post("", new EmptyRequest(), ct);
    public sealed record PeriodicGroupActions(AppClientPostAction<EmptyRequest, EmptyActionResult> PurgeLogs);
}