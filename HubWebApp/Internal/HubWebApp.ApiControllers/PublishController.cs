// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
public sealed partial class PublishController : Controller
{
    private readonly HubAppApi api;
    public PublishController(HubAppApi api)
    {
        this.api = api;
    }

    [HttpPost]
    public Task<ResultContainer<XtiVersionModel>> BeginPublish([FromBody] PublishVersionRequest requestData, CancellationToken ct)
    {
        return api.Publish.BeginPublish.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<XtiVersionModel>> EndPublish([FromBody] PublishVersionRequest requestData, CancellationToken ct)
    {
        return api.Publish.EndPublish.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<XtiVersionModel[]>> GetVersions([FromBody] AppKeyRequest requestData, CancellationToken ct)
    {
        return api.Publish.GetVersions.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<XtiVersionModel>> NewVersion([FromBody] NewVersionRequest requestData, CancellationToken ct)
    {
        return api.Publish.NewVersion.Execute(requestData, ct);
    }
}