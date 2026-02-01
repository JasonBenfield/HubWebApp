using XTI_Core;

namespace XTI_HubWebAppApiActions.AppInstall;

public sealed class AddInstallationUserAction : AppAction<AddInstallationUserRequest, AppUserModel>
{
    private readonly EfHubDB appFactory;
    private readonly IClock clock;
    private readonly IHashedPasswordFactory hashedPasswordFactory;

    public AddInstallationUserAction(EfHubDB appFactory, IClock clock, IHashedPasswordFactory hashedPasswordFactory)
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
            clock.Now(),
            stoppingToken
        );
        return systemUser.ToModel();
    }
}