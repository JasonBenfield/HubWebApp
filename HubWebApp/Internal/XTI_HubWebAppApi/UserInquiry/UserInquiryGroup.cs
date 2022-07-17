namespace XTI_HubWebAppApi.UserInquiry;

public sealed class UserInquiryGroup : AppApiGroupWrapper
{
    public UserInquiryGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        GetUser = source.AddAction(nameof(GetUser), () => sp.GetRequiredService<GetUserAction>());
    }

    public AppApiAction<int, AppUserModel> GetUser { get; }
}