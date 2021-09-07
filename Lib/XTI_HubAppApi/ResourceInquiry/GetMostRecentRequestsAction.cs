using System.Linq;
using System.Threading.Tasks;
using XTI_App;
using XTI_App.Abstractions;
using XTI_App.Api;

namespace XTI_HubAppApi.ResourceInquiry
{
    public sealed class GetMostRecentRequestsAction : AppAction<GetResourceLogRequest, AppRequestExpandedModel[]>
    {
        private readonly AppFromPath appFromPath;

        public GetMostRecentRequestsAction(AppFromPath appFromPath)
        {
            this.appFromPath = appFromPath;
        }

        public async Task<AppRequestExpandedModel[]> Execute(GetResourceLogRequest model)
        {
            var app = await appFromPath.Value();
            var versionKey = AppVersionKey.Parse(model.VersionKey);
            var version = await app.Version(versionKey);
            var resource = await version.Resource(model.ResourceID);
            var requests = await resource.MostRecentRequests(model.HowMany);
            return requests.ToArray();
        }
    }
}
