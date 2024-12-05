// Generated Code
namespace XTI_HubAppClient;
public sealed partial class PermanentLogGroup : AppClientGroup
{
    public PermanentLogGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "PermanentLog")
    {
        Actions = new PermanentLogGroupActions(LogBatch: CreatePostAction<LogBatchModel, EmptyActionResult>("LogBatch"), LogSessionDetails: CreatePostAction<LogSessionDetailsRequest, EmptyActionResult>("LogSessionDetails"));
    }

    public PermanentLogGroupActions Actions { get; }

    public Task<EmptyActionResult> LogBatch(LogBatchModel model, CancellationToken ct = default) => Actions.LogBatch.Post("", model, ct);
    public Task<EmptyActionResult> LogSessionDetails(LogSessionDetailsRequest model, CancellationToken ct = default) => Actions.LogSessionDetails.Post("", model, ct);
    public sealed record PermanentLogGroupActions(AppClientPostAction<LogBatchModel, EmptyActionResult> LogBatch, AppClientPostAction<LogSessionDetailsRequest, EmptyActionResult> LogSessionDetails);
}