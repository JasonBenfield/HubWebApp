namespace XTI_HubWebAppApi.UserInquiry;

public sealed class UserInquiryGroup : AppApiGroupWrapper
{
    public UserInquiryGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        GetUser = source.AddAction(nameof(GetUser), () => sp.GetRequiredService<GetUserAction>());
        GetUserOrAnon = source.AddAction(nameof(GetUserOrAnon), () => sp.GetRequiredService<GetUserOrAnonAction>());
        GetUserAuthenticators = source.AddAction(nameof(GetUserAuthenticators), () => sp.GetRequiredService<GetUserAuthenticatorsAction>());
        GetUsers = source.AddAction(nameof(GetUsers), () => sp.GetRequiredService<GetUsersAction>());
    }

    public AppApiAction<AppUserIDRequest, AppUserModel> GetUser { get; }
    public AppApiAction<AppUserNameRequest, AppUserModel> GetUserOrAnon { get; }
    public AppApiAction<AppUserIDRequest, UserAuthenticatorModel[]> GetUserAuthenticators { get; }
    public AppApiAction<EmptyRequest, AppUserModel[]> GetUsers { get; }
}