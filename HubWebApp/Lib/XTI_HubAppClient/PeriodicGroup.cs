// Generated Code
namespace XTI_HubAppClient;
public sealed partial class PeriodicGroup : AppClientGroup
{
    public PeriodicGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "Periodic")
    {
        Actions = new PeriodicGroupActions(DeleteExpiredStoredObjects: CreatePostAction<EmptyRequest, EmptyActionResult>("DeleteExpiredStoredObjects"), EndExpiredSessions: CreatePostAction<EmptyRequest, EmptyActionResult>("EndExpiredSessions"), PurgeLogs: CreatePostAction<EmptyRequest, EmptyActionResult>("PurgeLogs"));
    }

    public PeriodicGroupActions Actions { get; }

    public Task<EmptyActionResult> DeleteExpiredStoredObjects(CancellationToken ct = default) => Actions.DeleteExpiredStoredObjects.Post("", new EmptyRequest(), ct);
    public Task<EmptyActionResult> EndExpiredSessions(CancellationToken ct = default) => Actions.EndExpiredSessions.Post("", new EmptyRequest(), ct);
    public Task<EmptyActionResult> PurgeLogs(CancellationToken ct = default) => Actions.PurgeLogs.Post("", new EmptyRequest(), ct);
    public sealed record PeriodicGroupActions(AppClientPostAction<EmptyRequest, EmptyActionResult> DeleteExpiredStoredObjects, AppClientPostAction<EmptyRequest, EmptyActionResult> EndExpiredSessions, AppClientPostAction<EmptyRequest, EmptyActionResult> PurgeLogs);
}