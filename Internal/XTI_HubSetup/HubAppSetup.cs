using System.Linq;
using System.Threading.Tasks;
using XTI_App.Abstractions;
using XTI_Hub;
using XTI_HubAppApi;
using XTI_HubAppApi.AppInstall;

namespace XTI_HubSetup
{
    public sealed class HubAppSetup : IAppSetup
    {
        private readonly AppFactory appFactory;
        private readonly HubAppApiFactory apiFactory;
        private readonly VersionReader versionReader;

        public HubAppSetup(AppFactory appFactory, HubAppApiFactory apiFactory, VersionReader versionReader)
        {
            this.appFactory = appFactory;
            this.apiFactory = apiFactory;
            this.versionReader = versionReader;
        }

        public async Task Run(AppVersionKey versionKey)
        {
            var hubApi = apiFactory.CreateForSuperUser();
            var versions = await versionReader.Versions();
            var template = apiFactory.CreateTemplate();
            var request = new RegisterAppRequest
            {
                AppTemplate = template.ToModel(),
                VersionKey = versionKey,
                Versions = versions
            };
            await hubApi.Install.RegisterApp.Invoke(request);
            var apps = await appFactory.Apps.All();
            var webApps = apps.Where(a => a.Key().Type.Equals(AppType.Values.WebApp));
            var systemUsers = await appFactory.SystemUsers.SystemUsers(HubInfo.AppKey);
            foreach (var webApp in webApps)
            {
                var manageCacheRole = await webApp.Role(AppRoleName.ManageUserCache);
                var defaultModifier = await webApp.DefaultModifier();
                foreach (var systemUser in systemUsers)
                {
                    var assignedRoles = await systemUser.AssignedRoles(defaultModifier);
                    if (!assignedRoles.Any(r => r.ID.Equals(manageCacheRole.ID)))
                    {
                        await systemUser.AddRole(manageCacheRole);
                    }
                }
            }
        }
    }
}
