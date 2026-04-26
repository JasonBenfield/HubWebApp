using Microsoft.EntityFrameworkCore;
using XTI_Hub.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class EfAppCommands
{
    private readonly EfHubDB db;

    internal EfAppCommands(EfHubDB db)
    {
        this.db = db;
    }

    internal async Task<EfAppCommand> Add
    (
        AppEntity app,
        EfInstallLocation efLocation,
        AppCommandName commandName,
        string serializedRequest,
        DateTimeOffset timeAdded,
        DateTimeOffset timeStarted,
        CancellationToken ct
    )
    {
        var command = new AppCommandEntity
        {
            AppID = app.ID,
            LocationID = efLocation.ID,
            CommandName = commandName.Value,
            SerializedRequest = serializedRequest,
            TimeAdded = timeAdded,
            Status = timeStarted.Year == 9999 ? AppCommandStatus.Values.Started : AppCommandStatus.Values.Pending,
            TimeStarted = timeStarted,
            TimeEnded = DateTimeOffset.MaxValue
        };
        await db.Context.AppCommands.Create(command, ct);
        return new EfAppCommand(db, command);
    }

    public async Task<EfAppCommand> Command(int commandID, CancellationToken ct)
    {
        var command = await db.Context.AppCommands.Retrieve()
            .Where(c => c.ID == commandID)
            .FirstOrDefaultAsync(ct);
        return new EfAppCommand(db, command ?? throw new Exception($"Command {commandID} not found."));
    }

    internal async Task<EfAppCommand> Command(AppEntity app, int commandID, CancellationToken ct)
    {
        var command = await db.Context.AppCommands.Retrieve()
            .Where(c => c.ID == commandID)
            .FirstOrDefaultAsync(ct);
        if (command == null)
        {
            throw new Exception($"Command {commandID} not found.");
        }
        if (command.AppID != app.ID)
        {
            throw new Exception($"Command {commandID} does not belong to app {app.ID}.");
        }
        return new EfAppCommand(db, command);
    }

    internal async Task<EfAppCommand> InstallCommandOrDefault(InstallationEntity installation, CancellationToken ct)
    {
        var commandIDs = db.Context.AppCommandInstallations.Retrieve()
            .Where(ci => ci.InstallationID == installation.ID)
            .Select(ci => ci.CommandID);
        var command = await db.Context.AppCommands.Retrieve()
            .Where(c => commandIDs.Contains(c.ID))
            .FirstOrDefaultAsync(ct);
        return new EfAppCommand(db, command ?? new());
    }

    public async Task<EfAppCommand[]> GetPendingCommands(AppCommandName[] commandNames, string machineName, CancellationToken ct)
    {
        var commandNameValues = commandNames.Select(cn => cn.Value).ToArray();
        var locationIDs = db.Context.InstallLocations.Retrieve()
            .Where(l => l.QualifiedMachineName == machineName)
            .Select(l => l.ID);
        var pendingStatus = AppCommandStatus.Values.Pending.Value;
        var commands = await db.Context.AppCommands.Retrieve()
            .Where
            (
                c =>
                    locationIDs.Contains(c.LocationID) &&
                    commandNameValues.Contains(c.CommandName) &&
                    c.Status == pendingStatus
            )
            .OrderBy(c => c.TimeAdded)
            .ToArrayAsync(ct);
        return commands.Select(c => new EfAppCommand(db, c)).ToArray();
    }

    public async Task<EfAppCommand[]> GetPendingCommands(CancellationToken ct)
    {
        int[] statuses = [AppCommandStatus.Values.Pending.Value, AppCommandStatus.Values.Started.Value];
        var commands = await db.Context.AppCommands.Retrieve()
            .Where(c => statuses.Contains(c.Status))
            .OrderBy(c => c.TimeAdded)
            .ToArrayAsync(ct);
        return commands.Select(c => new EfAppCommand(db, c)).ToArray();
    }
}
