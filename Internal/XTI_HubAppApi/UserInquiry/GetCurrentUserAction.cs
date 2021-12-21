using XTI_App.Api;
using XTI_Hub;

namespace XTI_HubAppApi.UserInquiry;

public sealed class GetCurrentUserAction : AppAction<EmptyRequest, AppUserModel>
{
    private readonly AppFactory appFactory;
    private readonly IUserContext userContext;

    public GetCurrentUserAction(AppFactory appFactory, IUserContext userContext)
    {
        this.appFactory = appFactory;
        this.userContext = userContext;
    }

    public async Task<AppUserModel> Execute(EmptyRequest model)
    {
        var userFromContext = await userContext.User();
        var user = await appFactory.Users.User(userFromContext.ID.Value);
        return user.ToModel();
    }
}