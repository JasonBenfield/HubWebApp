using System.Threading.Tasks;
using XTI_App;
using XTI_App.Abstractions;
using XTI_App.Api;

namespace XTI_HubAppApi.VersionInquiry
{
    public sealed class GetVersionAction : AppAction<string, AppVersionModel>
    {
        private readonly AppFromPath appFromPath;

        public GetVersionAction(AppFromPath appFromPath)
        {
            this.appFromPath = appFromPath;
        }

        public async Task<AppVersionModel> Execute(string model)
        {
            var versionKey = AppVersionKey.Parse(model);
            var app = await appFromPath.Value();
            var version = await app.Version(versionKey);
            var versionModel = version.ToModel();
            return versionModel;
        }
    }
}
