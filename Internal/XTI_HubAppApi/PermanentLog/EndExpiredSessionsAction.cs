using XTI_App.Api;
using XTI_Core;
using XTI_Hub;

namespace XTI_HubAppApi.PermanentLog;

public sealed class EndExpiredSessionsAction : AppAction<EmptyRequest, EmptyActionResult>
{
    private readonly AppFactory appFactory;
    private readonly IClock clock;

    public EndExpiredSessionsAction(AppFactory appFactory, IClock clock)
    {
        this.appFactory = appFactory;
        this.clock = clock;
    }

    public async Task<EmptyActionResult> Execute(EmptyRequest model)
    {
        var timeRange = DateTimeRange.OnOrBefore(clock.Now().AddHours(-5));
        var activeSessions = await appFactory.Sessions.ActiveSessions(timeRange);
        foreach (var activeSession in activeSessions)
        {
            var mostRecentRequests = await activeSession.MostRecentRequests(1);
            if (mostRecentRequests.Any())
            {
                var mostRecentRequest = mostRecentRequests.First();
                if (mostRecentRequest.HappendOnOrBefore(timeRange.End))
                {
                    await activeSession.End(clock.Now());
                }
            }
            else
            {
                await activeSession.End(clock.Now());
            }
        }
        return new EmptyActionResult();
    }
}