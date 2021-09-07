using System.Linq;
using System.Threading.Tasks;
using XTI_App;
using XTI_App.Api;

namespace XTI_HubAppApi.AppInquiry
{
    public sealed class GetMostRecentErrorEventsAction : AppAction<int, AppEventModel[]>
    {
        private readonly AppFromPath appFromPath;

        public GetMostRecentErrorEventsAction(AppFromPath appFromPath)
        {
            this.appFromPath = appFromPath;
        }

        public async Task<AppEventModel[]> Execute(int howMany)
        {
            var app = await appFromPath.Value();
            var events = await app.MostRecentErrorEvents(howMany);
            return events.Select(evt => evt.ToModel()).ToArray();
        }
    }
}
