using System.Linq;
using System.Threading.Tasks;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_Hub;

namespace XTI_HubAppApi.AppPublish
{
    public sealed class GetVersionsAction : AppAction<AppKey, AppVersionModel[]>
    {
        private readonly AppFactory appFactory;

        public GetVersionsAction(AppFactory appFactory)
        {
            this.appFactory = appFactory;
        }

        public async Task<AppVersionModel[]> Execute(AppKey appKey)
        {
            var app = await appFactory.Apps.App(appKey);
            var versions = await app.Versions();
            var versionModels = versions.Select(v => v.ToModel()).ToArray();
            return versionModels;
        }
    }
}
