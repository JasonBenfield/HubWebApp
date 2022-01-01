using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_Core;

namespace XTI_Hub;

public sealed class AppRegistration
{
    private readonly AppFactory appFactory;
    private readonly IClock clock;

    public AppRegistration(AppFactory appFactory, IClock clock)
    {
        this.appFactory = appFactory;
        this.clock = clock;
    }

    public async Task Run(AppApiTemplateModel template, AppVersionKey versionKey, AppVersionModel[] versions)
    {
        await appFactory.Users.AddAnonIfNotExists(clock.Now());
        await tryAddUnknownApp();
        var appKey = template.AppKey;
        var app = await appFactory.Apps.AddOrUpdate(appKey, clock.Now());
        foreach (var versionModel in versions)
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
        var roleNames = template.RecursiveRoles()
            .Select(r => new AppRoleName(r))
            .Union(new[] { AppRoleName.DenyAccess })
            .Distinct();
        await app.SetRoles(roleNames);
        var version = await app.Version(versionKey);
        foreach (var groupTemplate in template.GroupTemplates)
        {
            await updateResourceGroupFromTemplate(app, version, groupTemplate);
        }
        await addAppModifier(appKey, app);
        await addManageCacheRoleToHubSystemUsers(app);
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
            var assignedRoles = await systemUser.Modifier(appModifier).AssignedRoles();
            if (!assignedRoles.Any(r => r.ID.Equals(hubAdminRole.ID)))
            {
                await systemUser.Modifier(appModifier).AddRole(hubAdminRole);
            }
        }
    }

    private async Task addManageCacheRoleToHubSystemUsers(App app)
    {
        var defaultModifier = await app.DefaultModifier();
        var hubSystemUsers = await appFactory.SystemUsers.SystemUsers(HubInfo.AppKey);
        var manageCacheRole = await app.AddRoleIfNotFound(AppRoleName.ManageUserCache);
        foreach (var hubSystemUser in hubSystemUsers)
        {
            var assignedRoles = await hubSystemUser.Modifier(defaultModifier).AssignedRoles();
            if (!assignedRoles.Any(r => r.ID.Equals(manageCacheRole.ID)))
            {
                await hubSystemUser.AddRole(manageCacheRole);
            }
        }
    }
}