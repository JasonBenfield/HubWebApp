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

    internal async Task<EfAppCommand> InstallCommandOrDefault(InstallationEntity installation, CancellationToken ct)
    {
        var commandIDs = db.Context.AppCommandInstallations.Retrieve()
            .Where(ci => ci.InstallationID == installation.ID)
            .Select(ci => ci.CommandID);
        var requestedInstallation = await db.Context.AppCommands.Retrieve()
            .Where(c => commandIDs.Contains(c.ID))
            .FirstOrDefaultAsync(ct);
        return new EfAppCommand(db, requestedInstallation ?? new());
    }

    public async Task<EfAppCommand[]> GetPendingCommands(AppCommandName[] commandNames, string machineName, CancellationToken ct)
    {
        var commandNameValues = commandNames.Select(cn => cn.Value).ToArray();
        var locationIDs = db.Context.InstallLocations.Retrieve()
            .Where(l => l.QualifiedMachineName == machineName)
            .Select(l => l.ID);
        var commands = await db.Context.AppCommands.Retrieve()
            .Where
            (
                c =>
                    locationIDs.Contains(c.LocationID) &&
                    commandNameValues.Contains(c.CommandName) &&
                    c.TimeStarted.Year == 9999
            )
            .OrderBy(c => c.TimeAdded)
            .ToArrayAsync(ct);
        return commands.Select(inst => new EfAppCommand(db, inst)).ToArray();
    }

}
