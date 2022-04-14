using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_Hub;
using XTI_Hub.Abstractions;

namespace XTI_HubAppApi.UserInquiry;

public sealed class GetUserByUserNameAction : AppAction<string, AppUserModel>
{
    private readonly AppFactory factory;

    public GetUserByUserNameAction(AppFactory factory)
    {
        this.factory = factory;
    }

    public async Task<AppUserModel> Execute(string userName)
    {
        var user = await factory.Users.UserByUserName(new AppUserName(userName));
        return user.ToModel();
    }
}