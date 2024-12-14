using XTI_HubWebAppApiActions.UserInquiry;

// Generated Code
namespace XTI_HubWebAppApi.UserInquiry;
public sealed partial class UserInquiryGroup : AppApiGroupWrapper
{
    internal UserInquiryGroup(AppApiGroup source, UserInquiryGroupBuilder builder) : base(source)
    {
        GetUser = builder.GetUser.Build();
        GetUserAuthenticators = builder.GetUserAuthenticators.Build();
        GetUserOrAnon = builder.GetUserOrAnon.Build();
        GetUsers = builder.GetUsers.Build();
        Configure();
    }

    partial void Configure();
    public AppApiAction<AppUserIDRequest, AppUserModel> GetUser { get; }
    public AppApiAction<AppUserIDRequest, UserAuthenticatorModel[]> GetUserAuthenticators { get; }
    public AppApiAction<AppUserNameRequest, AppUserModel> GetUserOrAnon { get; }
    public AppApiAction<EmptyRequest, AppUserModel[]> GetUsers { get; }
}