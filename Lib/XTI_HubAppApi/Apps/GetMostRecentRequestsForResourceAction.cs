using System.Linq;
using System.Threading.Tasks;
using XTI_App;
using XTI_App.Api;

namespace XTI_HubAppApi.Apps
{
    public sealed class GetMostRecentRequestsForResourceAction : AppAction<GetResourceLogRequest, AppRequestExpandedModel[]>
    {
        private readonly AppFromPath appFromPath;

        public GetMostRecentRequestsForResourceAction(AppFromPath appFromPath)
        {
            this.appFromPath = appFromPath;
        }

        public async Task<AppRequestExpandedModel[]> Execute(GetResourceLogRequest model)
        {
            var app = await appFromPath.Value();
            var version = await app.CurrentVersion();
            var resource = await version.Resource(model.ResourceID);
            var requests = await resource.MostRecentRequests(model.HowMany);
            return requests.ToArray();
        }
    }
}
