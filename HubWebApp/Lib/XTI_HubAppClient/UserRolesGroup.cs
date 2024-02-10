// Generated Code
namespace XTI_HubAppClient;
public sealed partial class UserRolesGroup : AppClientGroup
{
    public UserRolesGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "UserRoles")
    {
        Actions = new UserRolesGroupActions(DeleteUserRole: CreatePostAction<UserRoleIDRequest, EmptyActionResult>("DeleteUserRole"), Index: CreateGetAction<UserRoleQueryRequest>("Index"), GetUserRoleDetail: CreatePostAction<UserRoleIDRequest, UserRoleDetailModel>("GetUserRoleDetail"), UserRole: CreateGetAction<UserRoleIDRequest>("UserRole"));
    }

    public UserRolesGroupActions Actions { get; }

    public Task<EmptyActionResult> DeleteUserRole(UserRoleIDRequest model, CancellationToken ct = default) => Actions.DeleteUserRole.Post("", model, ct);
    public Task<UserRoleDetailModel> GetUserRoleDetail(UserRoleIDRequest model, CancellationToken ct = default) => Actions.GetUserRoleDetail.Post("", model, ct);
    public sealed record UserRolesGroupActions(AppClientPostAction<UserRoleIDRequest, EmptyActionResult> DeleteUserRole, AppClientGetAction<UserRoleQueryRequest> Index, AppClientPostAction<UserRoleIDRequest, UserRoleDetailModel> GetUserRoleDetail, AppClientGetAction<UserRoleIDRequest> UserRole);
}