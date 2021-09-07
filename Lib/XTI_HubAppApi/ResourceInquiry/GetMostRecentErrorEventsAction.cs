using System.Linq;
using System.Threading.Tasks;
using XTI_App;
using XTI_App.Abstractions;
using XTI_App.Api;

namespace XTI_HubAppApi.ResourceInquiry
{
    public sealed class GetMostRecentErrorEventsAction : AppAction<GetResourceLogRequest, AppEventModel[]>
    {
        private readonly AppFromPath appFromPath;

        public GetMostRecentErrorEventsAction(AppFromPath appFromPath)
        {
            this.appFromPath = appFromPath;
        }

        public async Task<AppEventModel[]> Execute(GetResourceLogRequest model)
        {
            var app = await appFromPath.Value();
            var versionKey = AppVersionKey.Parse(model.VersionKey);
            var version = await app.Version(versionKey);
            var resource = await version.Resource(model.ResourceID);
            var events = await resource.MostRecentErrorEvents(model.HowMany);
            return events.Select(evt => evt.ToModel()).ToArray();
        }
    }
}
