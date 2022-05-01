namespace XTI_HubAppApi.UserInquiry;

public sealed class UserInquiryGroup : AppApiGroupWrapper
{
    public UserInquiryGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        var actions = new AppApiActionFactory(source);
        GetUser = source.AddAction(actions.Action(nameof(GetUser), () => sp.GetRequiredService<GetUserAction>()));
        GetUserByUserName = source.AddAction(actions.Action(nameof(GetUserByUserName), () => sp.GetRequiredService<GetUserByUserNameAction>()));
        GetCurrentUser = source.AddAction
        (
            actions.Action
            (
                nameof(GetCurrentUser),
                ResourceAccess.AllowAuthenticated(),
                () => sp.GetRequiredService<GetCurrentUserAction>()
            )
        );
        RedirectToAppUser = source.AddAction(actions.Action(nameof(RedirectToAppUser), () => sp.GetRequiredService<RedirectToAppUserAction>()));
    }

    public AppApiAction<int, AppUserModel> GetUser { get; }
    public AppApiAction<string, AppUserModel> GetUserByUserName { get; }
    public AppApiAction<EmptyRequest, AppUserModel> GetCurrentUser { get; }
    public AppApiAction<RedirectToAppUserRequest, WebRedirectResult> RedirectToAppUser { get; }
}