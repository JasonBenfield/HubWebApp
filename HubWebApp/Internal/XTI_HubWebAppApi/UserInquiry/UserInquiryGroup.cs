namespace XTI_HubWebAppApi.UserInquiry;

public sealed class UserInquiryGroup : AppApiGroupWrapper
{
    public UserInquiryGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        GetUser = source.AddAction(nameof(GetUser), () => sp.GetRequiredService<GetUserAction>());
        GetUserByUserName = source.AddAction(nameof(GetUserByUserName), () => sp.GetRequiredService<GetUserByUserNameAction>());
        GetCurrentUser = source.AddAction
        (
            nameof(GetCurrentUser),
            () => sp.GetRequiredService<GetCurrentUserAction>(),
            access: ResourceAccess.AllowAuthenticated()
        );
        RedirectToAppUser = source.AddAction(nameof(RedirectToAppUser), () => sp.GetRequiredService<RedirectToAppUserAction>());
    }

    public AppApiAction<int, AppUserModel> GetUser { get; }
    public AppApiAction<string, AppUserModel> GetUserByUserName { get; }
    public AppApiAction<EmptyRequest, AppUserModel> GetCurrentUser { get; }
    public AppApiAction<RedirectToAppUserRequest, WebRedirectResult> RedirectToAppUser { get; }
}