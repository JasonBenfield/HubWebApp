// Generated Code
namespace XTI_HubAppClient;
public sealed partial class UserRolesGroup : AppClientGroup
{
    public UserRolesGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "UserRoles")
    {
        Actions = new UserRolesGroupActions(DeleteUserRole: CreatePostAction<UserRoleIDRequest, EmptyActionResult>("DeleteUserRole"), GetUserRoleDetail: CreatePostAction<UserRoleIDRequest, UserRoleDetailModel>("GetUserRoleDetail"), Index: CreateGetAction<UserRoleQueryRequest>("Index"), UserRole: CreateGetAction<UserRoleIDRequest>("UserRole"));
        Configure();
    }

    partial void Configure();
    public UserRolesGroupActions Actions { get; }

    public Task<EmptyActionResult> DeleteUserRole(UserRoleIDRequest requestData, CancellationToken ct = default) => Actions.DeleteUserRole.Post("", requestData, ct);
    public Task<UserRoleDetailModel> GetUserRoleDetail(UserRoleIDRequest requestData, CancellationToken ct = default) => Actions.GetUserRoleDetail.Post("", requestData, ct);
    public sealed record UserRolesGroupActions(AppClientPostAction<UserRoleIDRequest, EmptyActionResult> DeleteUserRole, AppClientPostAction<UserRoleIDRequest, UserRoleDetailModel> GetUserRoleDetail, AppClientGetAction<UserRoleQueryRequest> Index, AppClientGetAction<UserRoleIDRequest> UserRole);
}