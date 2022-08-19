// Generated Code
namespace XTI_HubAppClient;
public sealed partial class PermanentLogGroup : AppClientGroup
{
    public PermanentLogGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "PermanentLog")
    {
        Actions = new PermanentLogGroupActions(LogBatch: CreatePostAction<LogBatchModel, EmptyActionResult>("LogBatch"), EndExpiredSessions: CreatePostAction<EmptyRequest, EmptyActionResult>("EndExpiredSessions"));
    }

    public PermanentLogGroupActions Actions { get; }

    public Task<EmptyActionResult> LogBatch(LogBatchModel model, CancellationToken ct = default) => Actions.LogBatch.Post("", model, ct);
    public Task<EmptyActionResult> EndExpiredSessions(CancellationToken ct = default) => Actions.EndExpiredSessions.Post("", new EmptyRequest(), ct);
    public sealed record PermanentLogGroupActions(AppClientPostAction<LogBatchModel, EmptyActionResult> LogBatch, AppClientPostAction<EmptyRequest, EmptyActionResult> EndExpiredSessions);
}