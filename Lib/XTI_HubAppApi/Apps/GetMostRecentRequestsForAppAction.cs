using System.Linq;
using System.Threading.Tasks;
using XTI_App;
using XTI_App.Api;

namespace XTI_HubAppApi.Apps
{
    public sealed class GetMostRecentRequestsForAppAction : AppAction<int, AppRequestExpandedModel[]>
    {
        private readonly AppFromPath appFromPath;

        public GetMostRecentRequestsForAppAction(AppFromPath appFromPath)
        {
            this.appFromPath = appFromPath;
        }

        public async Task<AppRequestExpandedModel[]> Execute(int howMany)
        {
            var app = await appFromPath.Value();
            var requests = await app.MostRecentRequests(howMany);
            return requests.ToArray();
        }
    }
}
