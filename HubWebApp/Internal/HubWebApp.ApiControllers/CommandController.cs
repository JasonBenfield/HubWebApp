// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
public sealed partial class CommandController : Controller
{
    private readonly HubAppApi api;
    public CommandController(HubAppApi api)
    {
        this.api = api;
    }

    [HttpPost]
    public Task<ResultContainer<AppCommandModel>> GetCommand([FromBody] AppCommandIDRequest requestData, CancellationToken ct)
    {
        return api.Command.GetCommand.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppDeleteCommandDetailModel>> GetDeleteCommandDetail([FromBody] AppCommandIDRequest requestData, CancellationToken ct)
    {
        return api.Command.GetDeleteCommandDetail.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppInstallCommandDetailModel>> GetInstallationCommandDetail([FromBody] AppCommandIDRequest requestData, CancellationToken ct)
    {
        return api.Command.GetInstallationCommandDetail.Execute(requestData, ct);
    }

    public async Task<IActionResult> Index(AppCommandIDRequest requestData, CancellationToken ct)
    {
        var result = await api.Command.Index.Execute(requestData, ct);
        return View(result.Data!.ViewName);
    }
}