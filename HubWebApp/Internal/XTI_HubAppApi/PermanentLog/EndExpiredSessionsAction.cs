﻿using XTI_Core;

namespace XTI_HubAppApi.PermanentLog;

public sealed class EndExpiredSessionsAction : AppAction<EmptyRequest, EmptyActionResult>
{
    private readonly HubFactory appFactory;
    private readonly IClock clock;

    public EndExpiredSessionsAction(HubFactory appFactory, IClock clock)
    {
        this.appFactory = appFactory;
        this.clock = clock;
    }

    public async Task<EmptyActionResult> Execute(EmptyRequest model, CancellationToken stoppingToken)
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