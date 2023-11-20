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

    [HttpPost]
    public Task<ResultContainer<AppLogEntryModel>> GetLogEntryOrDefaultByKey([FromBody] string model, CancellationToken ct)
    {
        return api.Group("Logs").Action<string, AppLogEntryModel>("GetLogEntryOrDefaultByKey").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppLogEntryDetailModel>> GetLogEntryDetail([FromBody] int model, CancellationToken ct)
    {
        return api.Group("Logs").Action<int, AppLogEntryDetailModel>("GetLogEntryDetail").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppRequestDetailModel>> GetRequestDetail([FromBody] int model, CancellationToken ct)
    {
        return api.Group("Logs").Action<int, AppRequestDetailModel>("GetRequestDetail").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppSessionDetailModel>> GetSessionDetail([FromBody] int model, CancellationToken ct)
    {
        return api.Group("Logs").Action<int, AppSessionDetailModel>("GetSessionDetail").Execute(model, ct);
    }

    public async Task<IActionResult> Sessions(CancellationToken ct)
    {
        var result = await api.Group("Logs").Action<EmptyRequest, WebViewResult>("Sessions").Execute(new EmptyRequest(), ct);
        return View(result.Data!.ViewName);
    }

    public async Task<IActionResult> Session(SessionViewRequest model, CancellationToken ct)
    {
        var result = await api.Group("Logs").Action<SessionViewRequest, WebViewResult>("Session").Execute(model, ct);
        return View(result.Data!.ViewName);
    }

    public async Task<IActionResult> AppRequests(AppRequestQueryRequest model, CancellationToken ct)
    {
        var result = await api.Group("Logs").Action<AppRequestQueryRequest, WebViewResult>("AppRequests").Execute(model, ct);
        return View(result.Data!.ViewName);
    }

    public async Task<IActionResult> AppRequest(AppRequestRequest model, CancellationToken ct)
    {
        var result = await api.Group("Logs").Action<AppRequestRequest, WebViewResult>("AppRequest").Execute(model, ct);
        return View(result.Data!.ViewName);
    }

    public async Task<IActionResult> LogEntry(LogEntryRequest model, CancellationToken ct)
    {
        var result = await api.Group("Logs").Action<LogEntryRequest, WebViewResult>("LogEntry").Execute(model, ct);
        return View(result.Data!.ViewName);
    }

    public async Task<IActionResult> LogEntries(LogEntryQueryRequest model, CancellationToken ct)
    {
        var result = await api.Group("Logs").Action<LogEntryQueryRequest, WebViewResult>("LogEntries").Execute(model, ct);
        return View(result.Data!.ViewName);
    }
}