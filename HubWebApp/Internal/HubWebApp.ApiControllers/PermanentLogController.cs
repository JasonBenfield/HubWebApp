// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
public sealed partial class PermanentLogController : Controller
{
    private readonly HubAppApi api;
    public PermanentLogController(HubAppApi api)
    {
        this.api = api;
    }

    [HttpPost]
    public Task<ResultContainer<EmptyActionResult>> LogBatch([FromBody] LogBatchModel requestData, CancellationToken ct)
    {
        return api.PermanentLog.LogBatch.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<EmptyActionResult>> LogSessionDetails([FromBody] LogSessionDetailsRequest requestData, CancellationToken ct)
    {
        return api.PermanentLog.LogSessionDetails.Execute(requestData, ct);
    }
}