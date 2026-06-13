using XTI_Core;

namespace XTI_HubWebAppApiActions.App;

public sealed class AddInstallCommandAction : AppAction<InstallConfigurationIDRequest, AppInstallCommandDetailModel>
{
    private readonly AppFromPath appFromPath;
    private readonly EfHubDB db;
    private readonly IClock clock;

    public AddInstallCommandAction(AppFromPath appFromPath, EfHubDB db, IClock clock)
    {
        this.appFromPath = appFromPath;
        this.db = db;
        this.clock = clock;
    }

    public async Task<AppInstallCommandDetailModel> Execute(InstallConfigurationIDRequest requestData, CancellationToken stoppingToken)
    {
        var efApp = await appFromPath.Value(stoppingToken);
        var app = efApp.ToModel();
        var efCurrentVersion = await efApp.CurrentVersion(stoppingToken);
        var currentVersion = efCurrentVersion.Version.ToModel();
        var efInstallConfiguration = await db.InstallConfigurations.Configuration(requestData.ConfigurationID, stoppingToken);
        var installConfiguration = await efInstallConfiguration.ToModel(stoppingToken);
        var installRequest = new AddInstallCommandRequest
        (
            appKey: app.AppKey,
            versionKey: currentVersion.VersionKey,
            installConfigurationID: requestData.ConfigurationID,
            installAsCurrent: true,
            isAutoStartEnabled: false
        );
        var efLocation = await db.InstallLocations.AddIfNotFound(installConfiguration.Template.DestinationMachineName, stoppingToken);
        var efInstallCommand = await efApp.AddCommand
        (
            efLocation: efLocation,
            commandName: AppCommandName.Install,
            serializedRequest: installRequest.Serialize(),
            timeAdded: clock.Now(),
            timeStarted: DateTimeOffset.MaxValue,
            ct: stoppingToken
        );
        var installCommandDetail = await efInstallCommand.ToInstallCommandDetailModel(stoppingToken);
        return installCommandDetail;
    }
}