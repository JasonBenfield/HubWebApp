using XTI_HubWebAppApiActions.CurrentUser;

// Generated Code
#nullable enable
namespace XTI_HubWebAppApi.CurrentUser;
public sealed partial class CurrentUserGroupBuilder
{
    private readonly AppApiGroup source;
    internal CurrentUserGroupBuilder(AppApiGroup source)
    {
        this.source = source;
        ChangePassword = source.AddAction<ChangeCurrentUserPasswordForm, EmptyActionResult>("ChangePassword").WithExecution<ChangePasswordAction>();
        EditUser = source.AddAction<EditCurrentUserForm, AppUserModel>("EditUser").WithExecution<EditUserAction>();
        GetUser = source.AddAction<EmptyRequest, AppUserModel>("GetUser").WithExecution<GetUserAction>();
        Index = source.AddAction<EmptyRequest, WebViewResult>("Index").WithExecution<IndexAction>();
        Configure();
    }

    partial void Configure();
    public AppApiActionBuilder<ChangeCurrentUserPasswordForm, EmptyActionResult> ChangePassword { get; }
    public AppApiActionBuilder<EditCurrentUserForm, AppUserModel> EditUser { get; }
    public AppApiActionBuilder<EmptyRequest, AppUserModel> GetUser { get; }
    public AppApiActionBuilder<EmptyRequest, WebViewResult> Index { get; }

    public CurrentUserGroup Build() => new CurrentUserGroup(source, this);
}