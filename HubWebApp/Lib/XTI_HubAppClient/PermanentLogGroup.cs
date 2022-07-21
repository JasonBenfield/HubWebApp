// Generated Code
namespace XTI_HubAppClient;
public sealed partial class PermanentLogGroup : AppClientGroup
{
    public PermanentLogGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "PermanentLog")
    {
        Actions = new PermanentLogGroupActions(LogBatch: CreatePostAction<LogBatchModel, EmptyActionResult>("LogBatch"), EndExpiredSessions: CreatePostAction<EmptyRequest, EmptyActionResult>("EndExpiredSessions"));
    }

    public PermanentLogGroupActions Actions { get; }

    public Task<EmptyActionResult> LogBatch(LogBatchModel model) => Actions.LogBatch.Post("", model);
    public Task<EmptyActionResult> EndExpiredSessions() => Actions.EndExpiredSessions.Post("", new EmptyRequest());
    public sealed record PermanentLogGroupActions(AppClientPostAction<LogBatchModel, EmptyActionResult> LogBatch, AppClientPostAction<EmptyRequest, EmptyActionResult> EndExpiredSessions);
}