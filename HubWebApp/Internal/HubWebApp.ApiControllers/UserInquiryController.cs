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
    public Task<ResultContainer<AppUserModel>> GetUser([FromBody] int model, CancellationToken ct)
    {
        return api.Group("UserInquiry").Action<int, AppUserModel>("GetUser").Execute(model, ct);
    }
}