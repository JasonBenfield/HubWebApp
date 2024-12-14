using XTI_Core;

namespace XTI_HubWebAppApiActions.Periodic;

public sealed class PurgeLogsAction : AppAction<EmptyRequest, EmptyActionResult>
{
    private readonly HubFactory hubFactory;
    private readonly IClock clock;

    public PurgeLogsAction(HubFactory hubFactory, IClock clock)
    {
        this.hubFactory = hubFactory;
        this.clock = clock;
    }

    public async Task<EmptyActionResult> Execute(EmptyRequest model, CancellationToken ct)
    {
        await hubFactory.Sessions.PurgeLogs(clock.Now().AddDays(-90));
        return new EmptyActionResult();
    }
}