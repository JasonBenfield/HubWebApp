// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
public sealed partial class CommandsController : Controller
{
    private readonly HubAppApi api;
    public CommandsController(HubAppApi api)
    {
        this.api = api;
    }

    [HttpPost]
    public Task<ResultContainer<AppCommandSummaryModel[]>> GetCommandsInProgress(CancellationToken ct)
    {
        return api.Commands.GetCommandsInProgress.Execute(new EmptyRequest(), ct);
    }

    public async Task<IActionResult> Index(AppCommandIDRequest requestData, CancellationToken ct)
    {
        var result = await api.Commands.Index.Execute(requestData, ct);
        return View(result.Data!.ViewName);
    }
}