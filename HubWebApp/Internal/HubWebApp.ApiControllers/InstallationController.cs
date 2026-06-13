// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
public sealed partial class InstallationController : Controller
{
    private readonly HubAppApi api;
    public InstallationController(HubAppApi api)
    {
        this.api = api;
    }

    [HttpPost]
    public Task<ResultContainer<AppDeleteCommandDetailModel>> AddDeleteCommand([FromBody] InstallationIDRequest requestData, CancellationToken ct)
    {
        return api.Installation.AddDeleteCommand.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<EmptyActionResult>> BeginDelete([FromBody] InstallationIDRequest requestData, CancellationToken ct)
    {
        return api.Installation.BeginDelete.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<EmptyActionResult>> Deleted([FromBody] InstallationIDRequest requestData, CancellationToken ct)
    {
        return api.Installation.Deleted.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<InstallationDetailModel>> GetInstallationDetail([FromBody] InstallationIDRequest requestData, CancellationToken ct)
    {
        return api.Installation.GetInstallationDetail.Execute(requestData, ct);
    }

    public async Task<IActionResult> Index(InstallationViewRequest requestData, CancellationToken ct)
    {
        var result = await api.Installation.Index.Execute(requestData, ct);
        return View(result.Data!.ViewName);
    }

    [HttpPost]
    public Task<ResultContainer<EmptyActionResult>> Installed([FromBody] InstallationIDRequest requestData, CancellationToken ct)
    {
        return api.Installation.Installed.Execute(requestData, ct);
    }
}