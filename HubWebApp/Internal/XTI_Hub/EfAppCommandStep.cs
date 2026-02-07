using XTI_Core;
using XTI_Hub.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class EfAppCommandStep
{
    private readonly EfHubDB db;
    private readonly AppCommandStepEntity step;

    internal EfAppCommandStep(EfHubDB db, AppCommandStepEntity step)
    {
        this.db = db;
        this.step = step;
    }

    public Task End(DateTimeOffset timeEnded, string errorMessage, CancellationToken ct) =>
        db.Context.AppCommandSteps.Update
        (
            step,
            s =>
            {
                s.TimeEnded = timeEnded;
                s.ErrorMessage = new TruncatedText(errorMessage, 5000).Value;
            }
        );

    public AppCommandStepModel ToModel() =>
        new AppCommandStepModel
        (
            ID: step.ID,
            Activity: step.Activity,
            TimeStarted: step.TimeStarted,
            TimeEnded: step.TimeEnded,
            ErrorMessage: step.ErrorMessage
        );
}
