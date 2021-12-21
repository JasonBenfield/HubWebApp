// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
public class UserInquiryController : Controller
{
    private readonly HubAppApi api;
    public UserInquiryController(HubAppApi api)
    {
        this.api = api;
    }

    [HttpPost]
    public Task<ResultContainer<AppUserModel>> GetUser([FromBody] int model)
    {
        return api.Group("UserInquiry").Action<int, AppUserModel>("GetUser").Execute(model);
    }

    [HttpPost]
    public Task<ResultContainer<AppUserModel>> GetUserByUserName([FromBody] string model)
    {
        return api.Group("UserInquiry").Action<string, AppUserModel>("GetUserByUserName").Execute(model);
    }

    [HttpPost]
    public Task<ResultContainer<AppUserModel>> GetCurrentUser()
    {
        return api.Group("UserInquiry").Action<EmptyRequest, AppUserModel>("GetCurrentUser").Execute(new EmptyRequest());
    }

    public async Task<IActionResult> RedirectToAppUser(RedirectToAppUserRequest model)
    {
        var result = await api.Group("UserInquiry").Action<RedirectToAppUserRequest, WebRedirectResult>("RedirectToAppUser").Execute(model);
        return Redirect(result.Data.Url);
    }
}