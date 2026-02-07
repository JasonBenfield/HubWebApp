using Microsoft.EntityFrameworkCore;
using XTI_Core;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class EfAppCommandSteps
{
    private readonly EfHubDB db;

    internal EfAppCommandSteps(EfHubDB db)
    {
        this.db = db;
    }

    internal async Task<EfAppCommandStep> Add(AppCommandEntity requestedInstallation, string activity, DateTimeOffset timeStarted, CancellationToken ct)
    {
        var step = new AppCommandStepEntity
        {
            RequestedInstallationID = requestedInstallation.ID,
            Activity = new TruncatedText(activity, 1000).Value,
            TimeStarted = timeStarted,
            TimeEnded = DateTimeOffset.MaxValue,
            ErrorMessage = ""
        };
        await db.Context.AppCommandSteps.Create(step, ct);
        return new EfAppCommandStep(db, step);
    }

    public async Task<EfAppCommandStep> Step(int stepID, CancellationToken ct)
    {
        var step = await db.Context.AppCommandSteps.Retrieve()
            .Where(s => s.ID == stepID)
            .FirstOrDefaultAsync(ct);
        return new EfAppCommandStep(db, step ?? throw new Exception($"Requested Installation Step {stepID} not found."));
    }

    internal async Task<EfAppCommandStep[]> Steps(AppCommandEntity requestedInstallation, CancellationToken ct)
    {
        var steps = await db.Context.AppCommandSteps.Retrieve()
            .Where(s => s.RequestedInstallationID == requestedInstallation.ID)
            .ToArrayAsync(ct);
        return steps.Select(s => new EfAppCommandStep(db, s)).ToArray();
    }
}
