using System.Linq;
using System.Threading.Tasks;
using XTI_App;
using XTI_App.Api;
using XTI_Core;

namespace HubWebAppApi.PermanentLog
{
    public sealed class EndExpiredSessionsAction : AppAction<EmptyRequest, EmptyActionResult>
    {
        private readonly AppFactory appFactory;
        private readonly Clock clock;


        public EndExpiredSessionsAction(AppFactory appFactory, Clock clock)
        {
            this.appFactory = appFactory;
            this.clock = clock;
        }

        public async Task<EmptyActionResult> Execute(EmptyRequest model)
        {
            var timeRange = TimeRange.OnOrBefore(clock.Now().AddHours(-5));
            var activeSessions = await appFactory.Sessions().ActiveSessions(timeRange);
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
}
