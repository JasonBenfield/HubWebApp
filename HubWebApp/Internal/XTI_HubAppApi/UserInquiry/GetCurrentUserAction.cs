namespace XTI_HubAppApi.UserInquiry;

public sealed class GetCurrentUserAction : AppAction<EmptyRequest, AppUserModel>
{
    private readonly HubFactory appFactory;
    private readonly IUserContext userContext;

    public GetCurrentUserAction(HubFactory appFactory, IUserContext userContext)
    {
        this.appFactory = appFactory;
        this.userContext = userContext;
    }

    public async Task<AppUserModel> Execute(EmptyRequest model)
    {
        var userFromContext = await userContext.User();
        var user = await appFactory.Users.User(userFromContext.ID);
        return user.ToModel();
    }
}