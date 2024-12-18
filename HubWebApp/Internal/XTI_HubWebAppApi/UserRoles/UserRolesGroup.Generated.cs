using XTI_HubWebAppApiActions.UserRoles;

// Generated Code
#nullable enable
namespace XTI_HubWebAppApi.UserRoles;
public sealed partial class UserRolesGroup : AppApiGroupWrapper
{
    internal UserRolesGroup(AppApiGroup source, UserRolesGroupBuilder builder) : base(source)
    {
        DeleteUserRole = builder.DeleteUserRole.Build();
        GetUserRoleDetail = builder.GetUserRoleDetail.Build();
        Index = builder.Index.Build();
        UserRole = builder.UserRole.Build();
        Configure();
    }

    partial void Configure();
    public AppApiAction<UserRoleIDRequest, EmptyActionResult> DeleteUserRole { get; }
    public AppApiAction<UserRoleIDRequest, UserRoleDetailModel> GetUserRoleDetail { get; }
    public AppApiAction<UserRoleQueryRequest, WebViewResult> Index { get; }
    public AppApiAction<UserRoleIDRequest, WebViewResult> UserRole { get; }
}