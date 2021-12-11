using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_Core;
using XTI_Hub;

namespace XTI_HubAppApi.AppInstall
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
            var app = await appFactory.Apps.AddOrUpdate(appKey, clock.Now());
            foreach (var versionModel in model.Versions)
            {
                await app.AddVersionIfNotFound
                (
                    AppVersionKey.Parse(versionModel.VersionKey),
                    clock.Now(),
                    versionModel.Status,
                    versionModel.VersionType,
                    versionModel.Version()
                );
            }
            var roleNames = model.AppTemplate.RecursiveRoles()
                .Select(r => new AppRoleName(r))
                .Union(new[] { AppRoleName.DenyAccess })
                .Distinct();
            await app.SetRoles(roleNames);
            var versionKey = string.IsNullOrWhiteSpace(model.VersionKey)
                ? AppVersionKey.Current
                : AppVersionKey.Parse(model.VersionKey);
            var version = await app.Version(versionKey);
            foreach (var groupTemplate in model.AppTemplate.GroupTemplates)
            {
                await updateResourceGroupFromTemplate(app, version, groupTemplate);
            }
            await addAppModifier(appKey, app);
            await addManageCacheRoleToHubSystemUsers(app);
            return new EmptyActionResult();
        }

        private async Task tryAddAnonUser()
        {
            var anonUser = await appFactory.Users.User(AppUserName.Anon);
            if (anonUser.ID.IsNotValid())
            {
                await appFactory.Users.Add
                (
                    AppUserName.Anon,
                    new SystemHashedPassword(),
                    clock.Now()
                );
            }
        }

        private async Task tryAddUnknownApp()
        {
            var app = await appFactory.Apps.AddOrUpdate(AppKey.Unknown, clock.Now());
            var currentVersion = await app.CurrentVersion();
            var defaultModCategory = await app.ModCategory(ModifierCategoryName.Default);
            var group = await currentVersion.AddOrUpdateResourceGroup(ResourceGroupName.Unknown, defaultModCategory);
            await group.AddOrUpdateResource(ResourceName.Unknown, ResourceResultType.Values.None);
        }

        private static async Task updateResourceGroupFromTemplate(App app, AppVersion version, AppApiGroupTemplateModel groupTemplate)
        {
            var modCategoryName = new ModifierCategoryName(groupTemplate.ModCategory);
            var modCategory = await app.AddModCategoryIfNotFound(modCategoryName);
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
            var resource = await resourceGroup.AddOrUpdateResource(resourceName, actionTemplate.ResultType);
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

        private async Task addAppModifier(AppKey appKey, App app)
        {
            var hubApp = await appFactory.Apps.App(HubInfo.AppKey);
            var appModCategory = await hubApp.ModCategory(HubInfo.ModCategories.Apps);
            var appModifier = await appModCategory.AddOrUpdateModifier(app.ID.Value, app.Title);
            var systemUsers = await appFactory.SystemUsers.SystemUsers(appKey);
            var hubAdminRole = await hubApp.Role(AppRoleName.Admin);
            foreach (var systemUser in systemUsers)
            {
                var assignedRoles = await systemUser.AssignedRoles(appModifier);
                if (!assignedRoles.Any(r => r.ID.Equals(hubAdminRole.ID)))
                {
                    await systemUser.AddRole(hubAdminRole, appModifier);
                }
            }
        }

        private async Task addManageCacheRoleToHubSystemUsers(App app)
        {
            var defaultModifier = await app.DefaultModifier();
            var hubSystemUsers = await appFactory.SystemUsers.SystemUsers(HubInfo.AppKey);
            var manageCacheRole = await app.Role(AppRoleName.ManageUserCache);
            foreach (var hubSystemUser in hubSystemUsers)
            {
                var assignedRoles = await hubSystemUser.AssignedRoles(defaultModifier);
                if (!assignedRoles.Any(r => r.ID.Equals(manageCacheRole.ID)))
                {
                    await hubSystemUser.AddRole(manageCacheRole);
                }
            }
        }

        private class SystemHashedPassword : IHashedPassword
        {
            public bool Equals(string other) => false;

            public string Value() => new GeneratedKey().Value();
        }
    }
}
