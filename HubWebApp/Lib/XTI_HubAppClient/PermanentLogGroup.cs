// Generated Code
namespace XTI_HubAppClient;
public sealed partial class PermanentLogGroup : AppClientGroup
{
    public PermanentLogGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl) : base(httpClientFactory, xtiTokenAccessor, clientUrl, "PermanentLog")
    {
    }

    public Task<EmptyActionResult> LogBatch(LogBatchModel model) => Post<EmptyActionResult, LogBatchModel>("LogBatch", "", model);
    public Task<EmptyActionResult> EndExpiredSessions() => Post<EmptyActionResult, EmptyRequest>("EndExpiredSessions", "", new EmptyRequest());
}