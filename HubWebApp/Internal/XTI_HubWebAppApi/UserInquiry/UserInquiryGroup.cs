namespace XTI_HubWebAppApi.UserInquiry;

public sealed class UserInquiryGroup : AppApiGroupWrapper
{
    public UserInquiryGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        GetUser = source.AddAction(nameof(GetUser), () => sp.GetRequiredService<GetUserAction>());
        GetUserOrAnon = source.AddAction(nameof(GetUserOrAnon), () => sp.GetRequiredService<GetUserOrAnonAction>());
    }

    public AppApiAction<int, AppUserModel> GetUser { get; }
    public AppApiAction<string, AppUserModel> GetUserOrAnon { get; }
}