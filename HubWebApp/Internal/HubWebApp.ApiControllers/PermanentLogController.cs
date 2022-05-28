// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
public class PermanentLogController : Controller
{
    private readonly HubAppApi api;
    public PermanentLogController(HubAppApi api)
    {
        this.api = api;
    }

    [HttpPost]
    public Task<ResultContainer<EmptyActionResult>> LogBatch([FromBody] LogBatchModel model)
    {
        return api.Group("PermanentLog").Action<LogBatchModel, EmptyActionResult>("LogBatch").Execute(model);
    }

    [HttpPost]
    public Task<ResultContainer<EmptyActionResult>> EndExpiredSessions()
    {
        return api.Group("PermanentLog").Action<EmptyRequest, EmptyActionResult>("EndExpiredSessions").Execute(new EmptyRequest());
    }
}