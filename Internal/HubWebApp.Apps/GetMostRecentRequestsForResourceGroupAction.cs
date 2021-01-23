using System.Linq;
using System.Threading.Tasks;
using XTI_App;
using XTI_App.Api;

namespace HubWebApp.Apps
{
    public sealed class GetMostRecentRequestsForResourceGroupAction : AppAction<GetResourceGroupLogRequest, AppRequestExpandedModel[]>
    {
        private readonly AppFromPath appFromPath;

        public GetMostRecentRequestsForResourceGroupAction(AppFromPath appFromPath)
        {
            this.appFromPath = appFromPath;
        }

        public async Task<AppRequestExpandedModel[]> Execute(GetResourceGroupLogRequest model)
        {
            var app = await appFromPath.Value();
            var group = await app.ResourceGroup(model.GroupID);
            var requests = await group.MostRecentRequests(model.HowMany);
            return requests.ToArray();
        }
    }
}
