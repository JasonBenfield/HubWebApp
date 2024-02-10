// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
public sealed partial class UserInquiryController : Controller
{
    private readonly HubAppApi api;
    public UserInquiryController(HubAppApi api)
    {
        this.api = api;
    }

    [HttpPost]
    public Task<ResultContainer<AppUserModel>> GetUser([FromBody] AppUserIDRequest model, CancellationToken ct)
    {
        return api.Group("UserInquiry").Action<AppUserIDRequest, AppUserModel>("GetUser").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppUserModel>> GetUserOrAnon([FromBody] AppUserNameRequest model, CancellationToken ct)
    {
        return api.Group("UserInquiry").Action<AppUserNameRequest, AppUserModel>("GetUserOrAnon").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<UserAuthenticatorModel[]>> GetUserAuthenticators([FromBody] AppUserIDRequest model, CancellationToken ct)
    {
        return api.Group("UserInquiry").Action<AppUserIDRequest, UserAuthenticatorModel[]>("GetUserAuthenticators").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppUserModel[]>> GetUsers(CancellationToken ct)
    {
        return api.Group("UserInquiry").Action<EmptyRequest, AppUserModel[]>("GetUsers").Execute(new EmptyRequest(), ct);
    }
}