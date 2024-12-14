// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
[Route("odata/UserQuery")]
public sealed partial class UserQueryController : XtiODataController<UserGroupKey, ExpandedUser>
{
    public UserQueryController(HubAppApi api) : base(api.UserQuery)
    {
    }
}