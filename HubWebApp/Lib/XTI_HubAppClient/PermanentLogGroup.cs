// Generated Code
namespace XTI_HubAppClient;
public sealed partial class PermanentLogGroup : AppClientGroup
{
    public PermanentLogGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "PermanentLog")
    {
        Actions = new PermanentLogGroupActions(LogSessionDetails: CreatePostAction<LogSessionDetailsRequest, EmptyActionResult>("LogSessionDetails"));
        Configure();
    }

    partial void Configure();
    public PermanentLogGroupActions Actions { get; }

    public Task<EmptyActionResult> LogSessionDetails(LogSessionDetailsRequest requestData, CancellationToken ct = default) => Actions.LogSessionDetails.Post("", requestData, ct);
    public sealed record PermanentLogGroupActions(AppClientPostAction<LogSessionDetailsRequest, EmptyActionResult> LogSessionDetails);
}