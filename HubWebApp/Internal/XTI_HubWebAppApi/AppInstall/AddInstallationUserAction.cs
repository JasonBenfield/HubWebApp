using XTI_Core;

namespace XTI_HubWebAppApi.AppInstall;

public sealed class AddInstallationUserAction : AppAction<AddInstallationUserRequest, AppUserModel>
{
    private readonly HubFactory appFactory;
    private readonly IClock clock;
    private readonly IHashedPasswordFactory hashedPasswordFactory;

    public AddInstallationUserAction(HubFactory appFactory, IClock clock, IHashedPasswordFactory hashedPasswordFactory)
    {
        this.appFactory = appFactory;
        this.clock = clock;
        this.hashedPasswordFactory = hashedPasswordFactory;
    }

    public async Task<AppUserModel> Execute(AddInstallationUserRequest addRequest, CancellationToken stoppingToken)
    {
        var hashedPassword = hashedPasswordFactory.Create(addRequest.Password);
        var systemUser = await appFactory.Installers.AddOrUpdateInstaller
        (
            addRequest.MachineName,
            hashedPassword, 
            clock.Now()
        );
        return systemUser.ToModel();
    }
}