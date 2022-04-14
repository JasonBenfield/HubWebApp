// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
public class PublishController : Controller
{
    private readonly HubAppApi api;
    public PublishController(HubAppApi api)
    {
        this.api = api;
    }

    [HttpPost]
    public Task<ResultContainer<XtiVersionModel>> NewVersion([FromBody] NewVersionRequest model)
    {
        return api.Group("Publish").Action<NewVersionRequest, XtiVersionModel>("NewVersion").Execute(model);
    }

    [HttpPost]
    public Task<ResultContainer<XtiVersionModel>> BeginPublish([FromBody] PublishVersionRequest model)
    {
        return api.Group("Publish").Action<PublishVersionRequest, XtiVersionModel>("BeginPublish").Execute(model);
    }

    [HttpPost]
    public Task<ResultContainer<XtiVersionModel>> EndPublish([FromBody] PublishVersionRequest model)
    {
        return api.Group("Publish").Action<PublishVersionRequest, XtiVersionModel>("EndPublish").Execute(model);
    }

    [HttpPost]
    public Task<ResultContainer<XtiVersionModel[]>> GetVersions([FromBody] AppKey model)
    {
        return api.Group("Publish").Action<AppKey, XtiVersionModel[]>("GetVersions").Execute(model);
    }
}