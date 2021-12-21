using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_Core;
using XTI_Hub;

namespace XTI_HubAppApi.AppInstall;

public sealed class AddSystemUserAction : AppAction<AddSystemUserRequest, AppUserModel>
{
    private readonly AppFactory appFactory;
    private readonly IClock clock;
    private readonly IHashedPasswordFactory hashedPasswordFactory;

    public AddSystemUserAction(AppFactory appFactory, IClock clock, IHashedPasswordFactory hashedPasswordFactory)
    {
        this.appFactory = appFactory;
        this.clock = clock;
        this.hashedPasswordFactory = hashedPasswordFactory;
    }

    public async Task<AppUserModel> Execute(AddSystemUserRequest model)
    {
        var hashedPassword = hashedPasswordFactory.Create(model.Password);
        var systemUser = await appFactory.SystemUsers.AddOrUpdateSystemUser(model.AppKey, model.MachineName, hashedPassword, clock.Now());
        return systemUser.ToModel();
    }
}