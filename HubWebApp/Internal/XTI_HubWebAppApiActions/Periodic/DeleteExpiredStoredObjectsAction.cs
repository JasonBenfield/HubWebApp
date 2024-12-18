
using XTI_Core;

namespace XTI_HubWebAppApiActions.Periodic;

public sealed class DeleteExpiredStoredObjectsAction : AppAction<EmptyRequest, EmptyActionResult>
{
    private readonly HubFactory hubFactory;
    private readonly IClock clock;

    public DeleteExpiredStoredObjectsAction(HubFactory hubFactory, IClock clock)
    {
        this.hubFactory = hubFactory;
        this.clock = clock;
    }

    public async Task<EmptyActionResult> Execute(EmptyRequest model, CancellationToken stoppingToken)
    {
        await hubFactory.StoredObjects.DeleteExpired(clock.Now());
        return new EmptyActionResult();
    }
}
