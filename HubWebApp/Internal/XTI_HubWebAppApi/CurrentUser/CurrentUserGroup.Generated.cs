using XTI_HubWebAppApiActions.CurrentUser;

// Generated Code
#nullable enable
namespace XTI_HubWebAppApi.CurrentUser;
public sealed partial class CurrentUserGroup : AppApiGroupWrapper
{
    internal CurrentUserGroup(AppApiGroup source, CurrentUserGroupBuilder builder) : base(source)
    {
        ChangePassword = builder.ChangePassword.Build();
        EditUser = builder.EditUser.Build();
        GetUser = builder.GetUser.Build();
        Index = builder.Index.Build();
        Configure();
    }

    partial void Configure();
    public AppApiAction<ChangeCurrentUserPasswordForm, EmptyActionResult> ChangePassword { get; }
    public AppApiAction<EditCurrentUserForm, AppUserModel> EditUser { get; }
    public AppApiAction<EmptyRequest, AppUserModel> GetUser { get; }
    public AppApiAction<EmptyRequest, WebViewResult> Index { get; }
}