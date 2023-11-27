// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
[Route("odata/UserRoleQuery")]
public sealed partial class UserRoleQueryController : XtiODataController<UserRoleQueryRequest, ExpandedUserRole>
{
    public UserRoleQueryController(HubAppApi api) : base(api.Group("UserRoleQuery"))
    {
    }
}