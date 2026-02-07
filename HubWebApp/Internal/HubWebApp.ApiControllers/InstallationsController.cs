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
    public Task<ResultContainer<EmptyActionResult>> BeginDelete([FromBody] InstallationIDRequest requestData, CancellationToken ct)
    {
        return api.Installations.BeginDelete.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<InstallationModel>> BeginInstallation([FromBody] BeginInstallationRequest requestData, CancellationToken ct)
    {
        return api.Installations.BeginInstallation.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppCommandModel>> BeginRequestedInstallation([FromBody] AppCommandIDRequest requestData, CancellationToken ct)
    {
        return api.Installations.BeginRequestedInstallation.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppCommandStepModel>> BeginRequestedInstallationStep([FromBody] BeginAppCommandStepRequest requestData, CancellationToken ct)
    {
        return api.Installations.BeginRequestedInstallationStep.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<EmptyActionResult>> Deleted([FromBody] InstallationIDRequest requestData, CancellationToken ct)
    {
        return api.Installations.Deleted.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<InstallationActivitiesResult>> GetInstallationActivities([FromBody] GetInstallationActivitiesRequest requestData, CancellationToken ct)
    {
        return api.Installations.GetInstallationActivities.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<InstallationDetailModel>> GetInstallationDetail([FromBody] InstallationIDRequest requestData, CancellationToken ct)
    {
        return api.Installations.GetInstallationDetail.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppInstallCommandDetailModel>> GetRequestedInstallationDetail([FromBody] AppCommandIDRequest requestData, CancellationToken ct)
    {
        return api.Installations.GetRequestedInstallationDetail.Execute(requestData, ct);
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
    public Task<ResultContainer<EmptyActionResult>> Installed([FromBody] InstallationIDRequest requestData, CancellationToken ct)
    {
        return api.Installations.Installed.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<EmptyActionResult>> RequestDelete([FromBody] InstallationIDRequest requestData, CancellationToken ct)
    {
        return api.Installations.RequestDelete.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<EmptyActionResult>> RequestedInstallationEnded([FromBody] AppCommandIDRequest requestData, CancellationToken ct)
    {
        return api.Installations.RequestedInstallationEnded.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<EmptyRequest>> RequestedInstallationStepEnded([FromBody] AppCommandStepEndedRequest requestData, CancellationToken ct)
    {
        return api.Installations.RequestedInstallationStepEnded.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppInstallCommandDetailModel>> RequestInstallation([FromBody] AddAppInstallCommandRequest requestData, CancellationToken ct)
    {
        return api.Installations.RequestInstallation.Execute(requestData, ct);
    }
}