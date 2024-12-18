// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
public sealed partial class AppUserInquiryController : Controller
{
    private readonly HubAppApi api;
    public AppUserInquiryController(HubAppApi api)
    {
        this.api = api;
    }

    [HttpPost]
    public Task<ResultContainer<AppRoleModel[]>> GetAssignedRoles([FromBody] UserModifierKey requestData, CancellationToken ct)
    {
        return api.AppUserInquiry.GetAssignedRoles.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppRoleModel[]>> GetExplicitlyUnassignedRoles([FromBody] UserModifierKey requestData, CancellationToken ct)
    {
        return api.AppUserInquiry.GetExplicitlyUnassignedRoles.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<UserAccessModel>> GetExplicitUserAccess([FromBody] UserModifierKey requestData, CancellationToken ct)
    {
        return api.AppUserInquiry.GetExplicitUserAccess.Execute(requestData, ct);
    }

    public async Task<IActionResult> Index(AppUserIndexRequest requestData, CancellationToken ct)
    {
        var result = await api.AppUserInquiry.Index.Execute(requestData, ct);
        return View(result.Data!.ViewName);
    }
}