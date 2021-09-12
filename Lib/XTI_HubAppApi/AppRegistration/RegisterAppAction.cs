using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_Core;
using XTI_Hub;

namespace XTI_HubAppApi.AppRegistration
{
    public sealed class RegisterAppAction : AppAction<RegisterAppRequest, EmptyActionResult>
    {
        private readonly AppFactory appFactory;
        private readonly Clock clock;

        public RegisterAppAction(AppFactory appFactory, Clock clock)
        {
            this.appFactory = appFactory;
            this.clock = clock;
        }

        public async Task<EmptyActionResult> Execute(RegisterAppRequest model)
        {
            await tryAddAnonUser();
            await tryAddUnknownApp();
            var appKey = model.AppTemplate.AppKey;
            var app = await addOrUpdateApp(appKey);
            foreach (var versionModel in model.Versions)
            {
                await app.TryAddVersion
                (
                    AppVersionKey.Parse(versionModel.VersionKey),
                    clock.Now(),
                    versionModel.Status,
                    versionModel.VersionType,
                    versionModel.Version()
                );
            }
            var roleNames = model.AppTemplate.RecursiveRoles().Select(r => new AppRoleName(r));
            await app.SetRoles(roleNames);
            var version = await tryAddCurrentVersion(app);
            var versionKey = string.IsNullOrWhiteSpace(model.VersionKey)
                ? AppVersionKey.Current
                : AppVersionKey.Parse(model.VersionKey);
            if (!versionKey.Equals(AppVersionKey.Current))
            {
                version = await app.Version(versionKey);
            }
            foreach (var groupTemplate in model.AppTemplate.GroupTemplates)
            {
                await updateResourceGroupFromTemplate(app, version, groupTemplate);
            }
            return new EmptyActionResult();
        }

        private async Task tryAddAnonUser()
        {
            var anonUser = await appFactory.Users().User(AppUserName.Anon);
            if (anonUser.ID.IsNotValid())
            {
                await appFactory.Users().Add
                (
                    AppUserName.Anon,
                    new SystemHashedPassword(),
                    clock.Now()
                );
            }
        }

        private async Task tryAddUnknownApp()
        {
            var app = await addOrUpdateApp(AppKey.Unknown);
            var currentVersion = await tryAddCurrentVersion(app);
            var defaultModCategory = await app.TryAddModCategory(ModifierCategoryName.Default);
            await defaultModCategory.TryAddDefaultModifier();
            var group = await currentVersion.AddOrUpdateResourceGroup(ResourceGroupName.Unknown, defaultModCategory);
            await group.TryAddResource(ResourceName.Unknown, ResourceResultType.Values.None);
        }

        private async Task<App> addOrUpdateApp(AppKey appKey)
        {
            return await appFactory.Apps().AddOrUpdate(appKey, appKey.Name.DisplayText, clock.Now());
        }

        private async Task<AppVersion> tryAddCurrentVersion(App app)
        {
            var currentVersion = await app.CurrentVersion();
            if (!currentVersion.IsCurrent())
            {
                currentVersion = await app.StartNewMajorVersion(clock.Now());
                await currentVersion.Publishing();
                await currentVersion.Published();
            }
            return currentVersion;
        }

        private static async Task updateResourceGroupFromTemplate(App app, AppVersion version, AppApiGroupTemplateModel groupTemplate)
        {
            var modCategory = await app.TryAddModCategory(new ModifierCategoryName(groupTemplate.ModCategory));
            var groupName = new ResourceGroupName(groupTemplate.Name);
            var resourceGroup = await version.AddOrUpdateResourceGroup(groupName, modCategory);
            if (groupTemplate.IsAnonymousAllowed)
            {
                await resourceGroup.AllowAnonymous();
            }
            else
            {
                await resourceGroup.DenyAnonymous();
            }
            var allowedGroupRoles = await rolesFromNames(app, groupTemplate.Roles.Select(r => new AppRoleName(r)));
            await resourceGroup.SetRoleAccess(allowedGroupRoles);
            foreach (var actionTemplate in groupTemplate.ActionTemplates)
            {
                await updateResourceFromTemplate(app, resourceGroup, actionTemplate);
            }
        }

        private static async Task updateResourceFromTemplate(App app, ResourceGroup resourceGroup, AppApiActionTemplateModel actionTemplate)
        {
            var resourceName = new ResourceName(actionTemplate.Name);
            var resource = await resourceGroup.TryAddResource(resourceName, actionTemplate.ResultType);
            if (actionTemplate.IsAnonymousAllowed)
            {
                await resource.AllowAnonymous();
            }
            else
            {
                await resource.DenyAnonymous();
            }
            var allowedResourceRoles = await rolesFromNames(app, actionTemplate.Roles.Select(r => new AppRoleName(r)));
            await resource.SetRoleAccess(allowedResourceRoles);
        }

        private static async Task<IEnumerable<AppRole>> rolesFromNames(App app, IEnumerable<AppRoleName> roleNames)
        {
            var roles = new List<AppRole>();
            foreach (var roleName in roleNames)
            {
                var role = await app.Role(roleName);
                roles.Add(role);
            }
            return roles;
        }

        private class SystemHashedPassword : IHashedPassword
        {
            public bool Equals(string other) => false;

            public string Value() => new GeneratedKey().Value();
        }
    }
}
