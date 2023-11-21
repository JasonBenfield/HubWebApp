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
    public Task<ResultContainer<EmptyActionResult>> LogBatch([FromBody] LogBatchModel model, CancellationToken ct)
    {
        return api.Group("PermanentLog").Action<LogBatchModel, EmptyActionResult>("LogBatch").Execute(model, ct);
    }
}