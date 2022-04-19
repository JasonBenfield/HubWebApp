using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_Core;
using XTI_Hub;
using XTI_Hub.Abstractions;

namespace XTI_HubAppApi.AppInstall;

public sealed class AddInstallationUserAction : AppAction<AddInstallationUserRequest, AppUserModel>
{
    private readonly AppFactory appFactory;
    private readonly IClock clock;
    private readonly IHashedPasswordFactory hashedPasswordFactory;

    public AddInstallationUserAction(AppFactory appFactory, IClock clock, IHashedPasswordFactory hashedPasswordFactory)
    {
        this.appFactory = appFactory;
        this.clock = clock;
        this.hashedPasswordFactory = hashedPasswordFactory;
    }

    public async Task<AppUserModel> Execute(AddInstallationUserRequest model)
    {
        var hashedPassword = hashedPasswordFactory.Create(model.Password);
        var systemUser = await appFactory.InstallationUsers.AddOrUpdateInstallationUser
        (
            model.MachineName,
            hashedPassword, 
            clock.Now()
        );
        return systemUser.ToModel();
    }
}