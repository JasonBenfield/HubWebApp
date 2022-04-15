// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
public class UsersController : Controller
{
    private readonly HubAppApi api;
    public UsersController(HubAppApi api)
    {
        this.api = api;
    }

    public async Task<IActionResult> Index()
    {
        var result = await api.Group("Users").Action<EmptyRequest, WebViewResult>("Index").Execute(new EmptyRequest());
        return View(result.Data.ViewName);
    }

    [HttpPost]
    public Task<ResultContainer<AppUserModel[]>> GetUsers()
    {
        return api.Group("Users").Action<EmptyRequest, AppUserModel[]>("GetUsers").Execute(new EmptyRequest());
    }

    [HttpPost]
    public Task<ResultContainer<AppUserModel[]>> GetSystemUsers([FromBody] AppKey model)
    {
        return api.Group("Users").Action<AppKey, AppUserModel[]>("GetSystemUsers").Execute(model);
    }

    [HttpPost]
    public Task<ResultContainer<int>> AddOrUpdateUser([FromBody] AddUserModel model)
    {
        return api.Group("Users").Action<AddUserModel, int>("AddOrUpdateUser").Execute(model);
    }
}