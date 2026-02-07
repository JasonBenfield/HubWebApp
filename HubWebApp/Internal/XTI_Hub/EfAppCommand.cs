using Microsoft.EntityFrameworkCore;
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
                c.TimeEnded = timeEnded;
            },
            ct
        );

    internal async Task<EfInstallation> BeginInstallation(DateTimeOffset timeAdded, bool isCurrent, CancellationToken ct)
    {
        ThrowIfNotInstallCommand();
        var installRequest = AddAppInstallCommandRequest.Deserialize(command.SerializedRequest);
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
        var installRequest = AddAppInstallCommandRequest.Deserialize(command.SerializedRequest);
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
            Version: efAppVersion.Version.ToModel(),
            InstallConfiguration: configuration,
            Steps: efSteps.Select(s => s.ToModel()).ToArray(),
            Installations: efInstallations.Select(inst => inst.ToModel()).ToArray()
        );
    }

    private void ThrowIfNotInstallCommand()
    {
        if (!AppCommandName.Install.Equals(command.CommandName))
        {
            throw new Exception($"Command {command.ID} has command name '{command.CommandName}'");
        }
    }

    public AppCommandModel ToModel() =>
        new AppCommandModel
        (
            ID: command.ID,
            CommandName: new AppCommandName(command.CommandName),
            TimeStarted: command.TimeStarted,
            TimeEnded: command.TimeEnded
        );

}
