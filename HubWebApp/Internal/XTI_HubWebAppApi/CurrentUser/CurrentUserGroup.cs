namespace XTI_HubWebAppApi.CurrentUser;

public sealed class CurrentUserGroup : AppApiGroupWrapper
{
    public CurrentUserGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        ChangePassword = source.AddAction(nameof(ChangePassword), () => sp.GetRequiredService<ChangePasswordAction>());
        EditUser = source.AddAction(nameof(EditUser), () => sp.GetRequiredService<EditUserAction>());
        GetUser = source.AddAction(nameof(GetUser), () => sp.GetRequiredService<GetUserAction>());
        Index = source.AddAction(nameof(Index), () => sp.GetRequiredService<IndexAction>());
    }

    public AppApiAction<ChangeCurrentUserPasswordForm, EmptyActionResult> ChangePassword { get; }
    public AppApiAction<EditCurrentUserForm, AppUserModel> EditUser { get; }
    public AppApiAction<EmptyRequest, AppUserModel> GetUser { get; }
    public AppApiAction<EmptyRequest, WebViewResult> Index { get; }
}