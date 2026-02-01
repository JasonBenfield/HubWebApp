using XTI_Core;

namespace XTI_HubWebAppApiActions.Periodic;

public sealed class EndExpiredSessionsAction : AppAction<EmptyRequest, EmptyActionResult>
{
    private readonly EfHubDB appFactory;
    private readonly IClock clock;

    public EndExpiredSessionsAction(EfHubDB appFactory, IClock clock)
    {
        this.appFactory = appFactory;
        this.clock = clock;
    }

    public async Task<EmptyActionResult> Execute(EmptyRequest model, CancellationToken stoppingToken)
    {
        var timeRange = DateTimeRange.OnOrBefore(clock.Now().AddDays(-1));
        var activeSessions = await appFactory.Sessions.ActiveSessions(timeRange, stoppingToken);
        foreach (var activeSession in activeSessions)
        {
            var mostRecentRequests = await activeSession.MostRecentRequests(1, stoppingToken);
            if (mostRecentRequests.Any())
            {
                var mostRecentRequest = mostRecentRequests.First();
                if (mostRecentRequest.HappendOnOrBefore(timeRange.End))
                {
                    await activeSession.End(clock.Now(), stoppingToken);
                }
            }
            else
            {
                await activeSession.End(clock.Now(), stoppingToken);
            }
        }
        return new EmptyActionResult();
    }
}