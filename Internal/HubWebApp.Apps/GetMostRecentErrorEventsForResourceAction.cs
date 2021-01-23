using System.Linq;
using System.Threading.Tasks;
using XTI_App;
using XTI_App.Api;

namespace HubWebApp.Apps
{
    public sealed class GetMostRecentErrorEventsForResourceAction : AppAction<GetResourceLogRequest, AppEventModel[]>
    {
        private readonly AppFromPath appFromPath;

        public GetMostRecentErrorEventsForResourceAction(AppFromPath appFromPath)
        {
            this.appFromPath = appFromPath;
        }

        public async Task<AppEventModel[]> Execute(GetResourceLogRequest model)
        {
            var app = await appFromPath.Value();
            var resource = await app.Resource(model.ResourceID);
            var events = await resource.MostRecentErrorEvents(model.HowMany);
            return events.Select(evt => evt.ToModel()).ToArray();
        }
    }
}
