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

    public async Task<IActionResult> AppRequestsView(AppRequestQueryRequest requestData, CancellationToken ct)
    {
        var result = await api.Logs.AppRequestsView.Execute(requestData, ct);
        return View(result.Data!.ViewName);
    }

    public async Task<IActionResult> AppRequestView(AppRequestRequest requestData, CancellationToken ct)
    {
        var result = await api.Logs.AppRequestView.Execute(requestData, ct);
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

    public async Task<IActionResult> LogEntriesView(LogEntryQueryRequest requestData, CancellationToken ct)
    {
        var result = await api.Logs.LogEntriesView.Execute(requestData, ct);
        return View(result.Data!.ViewName);
    }

    public async Task<IActionResult> LogEntryView(LogEntryRequest requestData, CancellationToken ct)
    {
        var result = await api.Logs.LogEntryView.Execute(requestData, ct);
        return View(result.Data!.ViewName);
    }

    public async Task<IActionResult> SessionsView(CancellationToken ct)
    {
        var result = await api.Logs.SessionsView.Execute(new EmptyRequest(), ct);
        return View(result.Data!.ViewName);
    }

    public async Task<IActionResult> SessionView(SessionViewRequest requestData, CancellationToken ct)
    {
        var result = await api.Logs.SessionView.Execute(requestData, ct);
        return View(result.Data!.ViewName);
    }
}