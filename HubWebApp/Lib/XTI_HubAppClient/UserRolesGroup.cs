// Generated Code
namespace XTI_HubAppClient;
public sealed partial class UserRolesGroup : AppClientGroup
{
    public UserRolesGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "UserRoles")
    {
        Actions = new UserRolesGroupActions(Index: CreateGetAction<UserRoleQueryRequest>("Index"), GetUserRoleDetail: CreatePostAction<int, UserRoleDetailModel>("GetUserRoleDetail"));
    }

    public UserRolesGroupActions Actions { get; }

    public Task<UserRoleDetailModel> GetUserRoleDetail(int model, CancellationToken ct = default) => Actions.GetUserRoleDetail.Post("", model, ct);
    public sealed record UserRolesGroupActions(AppClientGetAction<UserRoleQueryRequest> Index, AppClientPostAction<int, UserRoleDetailModel> GetUserRoleDetail);
}