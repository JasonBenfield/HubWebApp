// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
public sealed partial class InstallationsController : Controller
{
    private readonly HubAppApi api;
    public InstallationsController(HubAppApi api)
    {
        this.api = api;
    }

    [HttpPost]
    public Task<ResultContainer<EmptyActionResult>> BeginDelete([FromBody] GetInstallationRequest requestData, CancellationToken ct)
    {
        return api.Installations.BeginDelete.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<EmptyActionResult>> Deleted([FromBody] GetInstallationRequest requestData, CancellationToken ct)
    {
        return api.Installations.Deleted.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<InstallationDetailModel>> GetInstallationDetail([FromBody] int requestData, CancellationToken ct)
    {
        return api.Installations.GetInstallationDetail.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppVersionInstallationModel[]>> GetPendingDeletes([FromBody] GetPendingDeletesRequest requestData, CancellationToken ct)
    {
        return api.Installations.GetPendingDeletes.Execute(requestData, ct);
    }

    public async Task<IActionResult> Index(InstallationQueryRequest requestData, CancellationToken ct)
    {
        var result = await api.Installations.Index.Execute(requestData, ct);
        return View(result.Data!.ViewName);
    }

    public async Task<IActionResult> Installation(InstallationViewRequest requestData, CancellationToken ct)
    {
        var result = await api.Installations.Installation.Execute(requestData, ct);
        return View(result.Data!.ViewName);
    }

    [HttpPost]
    public Task<ResultContainer<EmptyActionResult>> RequestDelete([FromBody] GetInstallationRequest requestData, CancellationToken ct)
    {
        return api.Installations.RequestDelete.Execute(requestData, ct);
    }
}