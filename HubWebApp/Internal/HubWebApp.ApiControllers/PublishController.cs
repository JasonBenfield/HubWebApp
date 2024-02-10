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
    public Task<ResultContainer<XtiVersionModel>> NewVersion([FromBody] NewVersionRequest model, CancellationToken ct)
    {
        return api.Group("Publish").Action<NewVersionRequest, XtiVersionModel>("NewVersion").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<XtiVersionModel>> BeginPublish([FromBody] PublishVersionRequest model, CancellationToken ct)
    {
        return api.Group("Publish").Action<PublishVersionRequest, XtiVersionModel>("BeginPublish").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<XtiVersionModel>> EndPublish([FromBody] PublishVersionRequest model, CancellationToken ct)
    {
        return api.Group("Publish").Action<PublishVersionRequest, XtiVersionModel>("EndPublish").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<XtiVersionModel[]>> GetVersions([FromBody] AppKeyRequest model, CancellationToken ct)
    {
        return api.Group("Publish").Action<AppKeyRequest, XtiVersionModel[]>("GetVersions").Execute(model, ct);
    }
}