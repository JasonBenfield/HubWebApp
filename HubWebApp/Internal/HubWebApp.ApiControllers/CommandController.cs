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

    public async Task<IActionResult> Index(AppCommandIDRequest requestData, CancellationToken ct)
    {
        var result = await api.Command.Index.Execute(requestData, ct);
        return View(result.Data!.ViewName);
    }
}