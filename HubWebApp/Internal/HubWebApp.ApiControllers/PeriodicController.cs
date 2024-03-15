// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
public sealed partial class PeriodicController : Controller
{
    private readonly HubAppApi api;
    public PeriodicController(HubAppApi api)
    {
        this.api = api;
    }

    [HttpPost]
    public Task<ResultContainer<EmptyActionResult>> DeactivateUsers(CancellationToken ct)
    {
        return api.Group("Periodic").Action<EmptyRequest, EmptyActionResult>("DeactivateUsers").Execute(new EmptyRequest(), ct);
    }

    [HttpPost]
    public Task<ResultContainer<EmptyActionResult>> DeleteExpiredStoredObjects(CancellationToken ct)
    {
        return api.Group("Periodic").Action<EmptyRequest, EmptyActionResult>("DeleteExpiredStoredObjects").Execute(new EmptyRequest(), ct);
    }

    [HttpPost]
    public Task<ResultContainer<EmptyActionResult>> EndExpiredSessions(CancellationToken ct)
    {
        return api.Group("Periodic").Action<EmptyRequest, EmptyActionResult>("EndExpiredSessions").Execute(new EmptyRequest(), ct);
    }

    [HttpPost]
    public Task<ResultContainer<EmptyActionResult>> PurgeLogs(CancellationToken ct)
    {
        return api.Group("Periodic").Action<EmptyRequest, EmptyActionResult>("PurgeLogs").Execute(new EmptyRequest(), ct);
    }
}