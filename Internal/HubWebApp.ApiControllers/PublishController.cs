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
    public Task<ResultContainer<AppVersionKey>> NextVersionKey()
    {
        return api.Group("Publish").Action<EmptyRequest, AppVersionKey>("NextVersionKey").Execute(new EmptyRequest());
    }

    [HttpPost]
    public Task<ResultContainer<AppVersionModel>> NewVersion([FromBody] NewVersionRequest model)
    {
        return api.Group("Publish").Action<NewVersionRequest, AppVersionModel>("NewVersion").Execute(model);
    }

    [HttpPost]
    public Task<ResultContainer<AppVersionModel>> BeginPublish([FromBody] PublishVersionRequest model)
    {
        return api.Group("Publish").Action<PublishVersionRequest, AppVersionModel>("BeginPublish").Execute(model);
    }

    [HttpPost]
    public Task<ResultContainer<AppVersionModel>> EndPublish([FromBody] PublishVersionRequest model)
    {
        return api.Group("Publish").Action<PublishVersionRequest, AppVersionModel>("EndPublish").Execute(model);
    }

    [HttpPost]
    public Task<ResultContainer<AppVersionModel[]>> GetVersions([FromBody] AppKey model)
    {
        return api.Group("Publish").Action<AppKey, AppVersionModel[]>("GetVersions").Execute(model);
    }
}