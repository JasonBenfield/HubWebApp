using XTI_HubWebAppApiActions.UserInquiry;

// Generated Code
namespace XTI_HubWebAppApi.UserInquiry;
public sealed partial class UserInquiryGroupBuilder
{
    private readonly AppApiGroup source;
    internal UserInquiryGroupBuilder(AppApiGroup source)
    {
        this.source = source;
        GetUser = source.AddAction<AppUserIDRequest, AppUserModel>("GetUser").WithExecution<GetUserAction>();
        GetUserAuthenticators = source.AddAction<AppUserIDRequest, UserAuthenticatorModel[]>("GetUserAuthenticators").WithExecution<GetUserAuthenticatorsAction>();
        GetUserOrAnon = source.AddAction<AppUserNameRequest, AppUserModel>("GetUserOrAnon").WithExecution<GetUserOrAnonAction>();
        GetUsers = source.AddAction<EmptyRequest, AppUserModel[]>("GetUsers").WithExecution<GetUsersAction>();
        Configure();
    }

    partial void Configure();
    public AppApiActionBuilder<AppUserIDRequest, AppUserModel> GetUser { get; }
    public AppApiActionBuilder<AppUserIDRequest, UserAuthenticatorModel[]> GetUserAuthenticators { get; }
    public AppApiActionBuilder<AppUserNameRequest, AppUserModel> GetUserOrAnon { get; }
    public AppApiActionBuilder<EmptyRequest, AppUserModel[]> GetUsers { get; }

    public UserInquiryGroup Build() => new UserInquiryGroup(source, this);
}