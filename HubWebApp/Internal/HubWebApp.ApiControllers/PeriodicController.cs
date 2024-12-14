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
        return api.Periodic.DeactivateUsers.Execute(new EmptyRequest(), ct);
    }

    [HttpPost]
    public Task<ResultContainer<EmptyActionResult>> DeleteExpiredStoredObjects(CancellationToken ct)
    {
        return api.Periodic.DeleteExpiredStoredObjects.Execute(new EmptyRequest(), ct);
    }

    [HttpPost]
    public Task<ResultContainer<EmptyActionResult>> EndExpiredSessions(CancellationToken ct)
    {
        return api.Periodic.EndExpiredSessions.Execute(new EmptyRequest(), ct);
    }

    [HttpPost]
    public Task<ResultContainer<EmptyActionResult>> PurgeLogs(CancellationToken ct)
    {
        return api.Periodic.PurgeLogs.Execute(new EmptyRequest(), ct);
    }
}