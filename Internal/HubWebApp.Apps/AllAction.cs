using System.Linq;
using System.Threading.Tasks;
using XTI_App;
using XTI_App.Api;

namespace HubWebApp.Apps
{
    public sealed class AllAction : AppAction<EmptyRequest, AppModel[]>
    {
        private readonly AppFactory appFactory;

        public AllAction(AppFactory appFactory)
        {
            this.appFactory = appFactory;
        }

        public async Task<AppModel[]> Execute(EmptyRequest model)
        {
            var apps = await appFactory.Apps().All();
            return apps.Select(a => a.ToAppModel()).ToArray();
        }
    }
}
