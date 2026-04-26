using XTI_Core;
using XTI_Hub.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class EfAppCommand
{
    private readonly EfHubDB db;
    private readonly AppCommandEntity command;

    internal EfAppCommand(EfHubDB db, AppCommandEntity command)
    {
        this.db = db;
        this.command = command;
    }

    internal Task Begin(DateTimeOffset timeStarted, CancellationToken ct) =>
        db.Context.AppCommands.Update
        (
            command,
            c =>
            {
                c.Status = AppCommandStatus.Values.Started;
                c.TimeStarted = timeStarted;
            },
            ct
        );

    internal Task<EfAppCommandStep> BeginStep(string activity, DateTimeOffset timeStarted, CancellationToken ct) =>
        db.AppCommandSteps.Add(command, activity, timeStarted, ct);

    internal Task End(DateTimeOffset timeEnded, CancellationToken ct) =>
        db.Context.AppCommands.Update
        (
            command,
            c =>
            {
                c.Status = AppCommandStatus.Values.Completed;
                c.TimeEnded = timeEnded;
            },
            ct
        );

    internal Task Failed(DateTimeOffset timeFailed, CancellationToken ct) =>
        db.Context.AppCommands.Update
        (
            command,
            c =>
            {
                c.Status = AppCommandStatus.Values.Failed;
                c.TimeEnded = timeFailed;
            },
            ct
        );

    internal async Task<EfInstallation> BeginInstallation(DateTimeOffset timeAdded, bool isCurrent, CancellationToken ct)
    {
        ThrowIfNotInstallCommand();
        var installRequest = AddInstallCommandRequest.Deserialize(command.SerializedRequest);
        var efConfiguration = await db.InstallConfigurations.Configuration(installRequest.InstallConfigurationID, ct);
        var configuration = await efConfiguration.ToModel(ct);
        var efLocation = await db.InstallLocations.Location(configuration.Template.DestinationMachineName, ct);
        var efApp = await db.Apps.App(command.AppID, ct);
        var efAppVersion = await efApp.VersionOrDefault(installRequest.ToAppVersionKey(), ct);
        var efInstallation = await db.Installations.NewInstallation
        (
            efLocation: efLocation,
            efAppVersion: efAppVersion,
            domain: configuration.Template.Domain,
            siteName: configuration.Template.SiteName,
            initialStatus: InstallStatus.Values.InstallStarted,
            isCurrent: isCurrent,
            timeAdded: timeAdded,
            ct: ct
        );
        await db.Context.AppCommandInstallations.Create
        (
            new AppCommandInstallationEntity
            {
                CommandID = command.ID,
                InstallationID = efInstallation.ID
            },
            ct
        );
        return efInstallation;
    }

    public async Task<AppInstallCommandDetailModel> ToInstallCommandDetailModel(CancellationToken ct)
    {
        ThrowIfNotInstallCommand();
        var efApp = await db.Apps.App(command.AppID, ct);
        var efLocation = await db.InstallLocations.Location(command.LocationID, ct);
        var installRequest = AddInstallCommandRequest.Deserialize(command.SerializedRequest);
        var efAppVersion = await efApp.VersionOrDefault(installRequest.ToAppVersionKey(), ct);
        var efConfiguration = await db.InstallConfigurations.Configuration(installRequest.InstallConfigurationID, ct);
        var configuration = await efConfiguration.ToModel(ct);
        var efSteps = await db.AppCommandSteps.Steps(command, ct);
        var installationIDs = db.Context.AppCommandInstallations.Retrieve()
            .Where(ci => ci.CommandID == command.ID)
            .Select(ci => ci.InstallationID);
        var efInstallations = await db.Installations.Installations(installationIDs, ct);
        return new AppInstallCommandDetailModel
        (
            Command: ToModel(),
            App: efApp.ToModel(),
            Location: efLocation.ToModel(),
            Steps: efSteps.Select(s => s.ToModel()).ToArray(),
            InstallRequest: installRequest,
            Version: efAppVersion.Version.ToModel(),
            InstallConfiguration: configuration,
            Installations: efInstallations.Select(inst => inst.ToModel()).ToArray()
        );
    }

    public async Task<AppDeleteCommandDetailModel> ToDeleteCommandDetailModel(CancellationToken ct)
    {
        ThrowIfNotDeleteCommand();
        var efApp = await db.Apps.App(command.AppID, ct);
        var efLocation = await db.InstallLocations.Location(command.LocationID, ct);
        var deleteRequest = XtiSerializer.Deserialize<InstallationIDRequest>(command.SerializedRequest);
        var efInstallation = await db.Installations.InstallationOrDefault(deleteRequest.InstallationID, ct);
        var efAppVersion = await efInstallation.AppVersion(ct);
        var efSteps = await db.AppCommandSteps.Steps(command, ct);
        return new AppDeleteCommandDetailModel
        (
            Command: ToModel(),
            App: efApp.ToModel(),
            Location: efLocation.ToModel(),
            Steps: efSteps.Select(s => s.ToModel()).ToArray(),
            Version: efAppVersion.Version.ToModel(),
            Installation: efInstallation.ToModel()
        );
    }

    private void ThrowIfNotInstallCommand()
    {
        if (!AppCommandName.Install.Equals(command.CommandName))
        {
            throw new Exception($"Command {command.ID} has command name '{command.CommandName}'");
        }
    }

    private void ThrowIfNotDeleteCommand()
    {
        if (!AppCommandName.Delete.Equals(command.CommandName))
        {
            throw new Exception($"Command {command.ID} has command name '{command.CommandName}'");
        }
    }

    public async Task<AppCommandSummaryModel> ToSummaryModel(CancellationToken ct)
    {
        var efApp = await db.Apps.App(command.AppID, ct);
        var efLocation = await db.InstallLocations.Location(command.LocationID, ct);
        return new AppCommandSummaryModel
        (
            Command: ToModel(),
            App: efApp.ToModel(),
            Location: efLocation.ToModel()
        );
    }

    public AppCommandModel ToModel() =>
        new AppCommandModel
        (
            ID: command.ID,
            CommandName: new AppCommandName(command.CommandName),
            Status: AppCommandStatus.Values.Value(command.Status),
            TimeAdded: command.TimeAdded,
            TimeStarted: command.TimeStarted,
            TimeEnded: command.TimeEnded
        );

}
