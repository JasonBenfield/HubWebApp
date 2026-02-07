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
        AppCommandName commandName,
        string serializedRequest,
        DateTimeOffset timeStarted,
        CancellationToken ct
    )
    {
        var command = new AppCommandEntity
        {
            AppID = app.ID,
            CommandName = commandName.Value,
            SerializedRequest = serializedRequest,
            TimeStarted = timeStarted,
            TimeEnded = DateTimeOffset.MaxValue
        };
        await db.Context.AppCommands.Create(command, ct);
        return new EfAppCommand(db, command);
    }

    public async Task<EfAppCommand> Command(int commandID, CancellationToken ct)
    {
        var command = await db.Context.AppCommands.Retrieve()
            .Where(ri => ri.ID == commandID)
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

}
