using XTI_HubWebAppApiActions.UserRoles;

// Generated Code
#nullable enable
namespace XTI_HubWebAppApi.UserRoles;
public sealed partial class UserRolesGroupBuilder
{
    private readonly AppApiGroup source;
    internal UserRolesGroupBuilder(AppApiGroup source)
    {
        this.source = source;
        DeleteUserRole = source.AddAction<UserRoleIDRequest, EmptyActionResult>("DeleteUserRole").WithExecution<DeleteUserRoleAction>();
        GetUserRoleDetail = source.AddAction<UserRoleIDRequest, UserRoleDetailModel>("GetUserRoleDetail").WithExecution<GetUserRoleDetailAction>();
        Index = source.AddAction<UserRoleQueryRequest, WebViewResult>("Index").WithExecution<IndexPage>();
        UserRole = source.AddAction<UserRoleIDRequest, WebViewResult>("UserRole").WithExecution<UserRolePage>();
        Configure();
    }

    partial void Configure();
    public AppApiActionBuilder<UserRoleIDRequest, EmptyActionResult> DeleteUserRole { get; }
    public AppApiActionBuilder<UserRoleIDRequest, UserRoleDetailModel> GetUserRoleDetail { get; }
    public AppApiActionBuilder<UserRoleQueryRequest, WebViewResult> Index { get; }
    public AppApiActionBuilder<UserRoleIDRequest, WebViewResult> UserRole { get; }

    public UserRolesGroup Build() => new UserRolesGroup(source, this);
}