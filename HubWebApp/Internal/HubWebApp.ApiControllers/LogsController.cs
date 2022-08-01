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

    public async Task<IActionResult> Sessions(CancellationToken ct)
    {
        var result = await api.Group("Logs").Action<EmptyRequest, WebViewResult>("Sessions").Execute(new EmptyRequest(), ct);
        return View(result.Data.ViewName);
    }

    public async Task<IActionResult> Requests(RequestQueryRequest model, CancellationToken ct)
    {
        var result = await api.Group("Logs").Action<RequestQueryRequest, WebViewResult>("Requests").Execute(model, ct);
        return View(result.Data.ViewName);
    }

    public async Task<IActionResult> LogEntries(CancellationToken ct)
    {
        var result = await api.Group("Logs").Action<EmptyRequest, WebViewResult>("LogEntries").Execute(new EmptyRequest(), ct);
        return View(result.Data.ViewName);
    }
}