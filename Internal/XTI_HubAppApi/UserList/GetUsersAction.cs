using XTI_App.Api;
using XTI_Hub;

namespace XTI_HubAppApi.UserList;

public sealed class GetUsersAction : AppAction<EmptyRequest, AppUserModel[]>
{
    private readonly AppFactory factory;

    public GetUsersAction(AppFactory factory)
    {
        this.factory = factory;
    }

    public async Task<AppUserModel[]> Execute(EmptyRequest model)
    {
        var users = await factory.Users.Users();
        return users.Select(u => u.ToModel()).ToArray();
    }
}