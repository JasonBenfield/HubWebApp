// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
public sealed partial class LogsController : Controller
{
    private readonly HubAppApi api;
    public LogsController(HubAppApi api)
    {
        this.api = api;
    }

    public async Task<IActionResult> AppRequest(AppRequestRequest requestData, CancellationToken ct)
    {
        var result = await api.Logs.AppRequest.Execute(requestData, ct);
        return View(result.Data!.ViewName);
    }

    public async Task<IActionResult> AppRequests(AppRequestQueryRequest requestData, CancellationToken ct)
    {
        var result = await api.Logs.AppRequests.Execute(requestData, ct);
        return View(result.Data!.ViewName);
    }

    [HttpPost]
    public Task<ResultContainer<AppLogEntryDetailModel>> GetLogEntryDetail([FromBody] int requestData, CancellationToken ct)
    {
        return api.Logs.GetLogEntryDetail.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppLogEntryModel>> GetLogEntryOrDefaultByKey([FromBody] string requestData, CancellationToken ct)
    {
        return api.Logs.GetLogEntryOrDefaultByKey.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppRequestDetailModel>> GetRequestDetail([FromBody] int requestData, CancellationToken ct)
    {
        return api.Logs.GetRequestDetail.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppSessionDetailModel>> GetSessionDetail([FromBody] int requestData, CancellationToken ct)
    {
        return api.Logs.GetSessionDetail.Execute(requestData, ct);
    }

    public async Task<IActionResult> LogEntries(LogEntryQueryRequest requestData, CancellationToken ct)
    {
        var result = await api.Logs.LogEntries.Execute(requestData, ct);
        return View(result.Data!.ViewName);
    }

    public async Task<IActionResult> LogEntry(LogEntryRequest requestData, CancellationToken ct)
    {
        var result = await api.Logs.LogEntry.Execute(requestData, ct);
        return View(result.Data!.ViewName);
    }

    public async Task<IActionResult> Session(SessionViewRequest requestData, CancellationToken ct)
    {
        var result = await api.Logs.Session.Execute(requestData, ct);
        return View(result.Data!.ViewName);
    }

    public async Task<IActionResult> Sessions(CancellationToken ct)
    {
        var result = await api.Logs.Sessions.Execute(new EmptyRequest(), ct);
        return View(result.Data!.ViewName);
    }
}