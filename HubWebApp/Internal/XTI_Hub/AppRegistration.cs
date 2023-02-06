using XTI_App.Abstractions;
using XTI_App.Api;

namespace XTI_Hub;

public sealed class AppRegistration
{
    private readonly HubFactory hubFactory;

    public AppRegistration(HubFactory hubFactory)
    {
        this.hubFactory = hubFactory;
    }

    public async Task<AppModel> Run(AppApiTemplateModel template, AppVersionKey versionKey)
    {
        var appKey = template.AppKey;
        var app = await hubFactory.Apps.App(appKey);
        var roleNames = template.RecursiveRoles()
            .Select(r => new AppRoleName(r))
            .Union(new[] { AppRoleName.DenyAccess })
            .Distinct();
        await app.SetRoles(roleNames);
        var version = await app.AddVersionIfNotFound(versionKey);
        foreach (var groupTemplate in template.GroupTemplates)
        {
            await UpdateResourceGroupFromTemplate(app, version, groupTemplate);
        }
        var modifier = await AddAppModifier(appKey, app);
        await AddManageCacheRoleToHubSystemUsers(app);
        return app.ToModel();
    }

    private static async Task UpdateResourceGroupFromTemplate(App app, AppVersion appVersion, AppApiGroupTemplateModel groupTemplate)
    {
        var modCategoryName = new ModifierCategoryName(groupTemplate.ModCategory);
        var modCategory = await app.AddOrUpdateModCategory(modCategoryName);
        var groupName = new ResourceGroupName(groupTemplate.Name);
        var resourceGroup = await appVersion.AddOrUpdateResourceGroup(groupName, modCategory);
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

    private async Task<Modifier> AddAppModifier(AppKey appKey, App app)
    {
        var hubApp = await hubFactory.Apps.App(HubInfo.AppKey);
        var appModCategory = await hubApp.ModCategory(HubInfo.ModCategories.Apps);
        var appModel = app.ToModel();
        var appModifier = await appModCategory.AddOrUpdateModifier
        (
            appModel.PublicKey,
            appModel.ID,
            appKey.Format()
        );
        var systemUsers = await hubFactory.SystemUsers.SystemUsers(appKey);
        var hubAdminRole = await hubApp.Role(AppRoleName.Admin);
        foreach (var systemUser in systemUsers)
        {
            await systemUser.Modifier(appModifier).AssignRole(hubAdminRole);
        }
        return appModifier;
    }

    private async Task AddManageCacheRoleToHubSystemUsers(App app)
    {
        var hubSystemUsers = await hubFactory.SystemUsers.SystemUsers(HubInfo.AppKey);
        var manageCacheRole = await app.AddOrUpdateRole(AppRoleName.ManageUserCache);
        foreach (var hubSystemUser in hubSystemUsers)
        {
            await hubSystemUser.AssignRole(manageCacheRole);
        }
    }
}