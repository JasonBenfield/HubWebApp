using XTI_App.Abstractions;
using XTI_App.Api;

namespace XTI_Hub;

public sealed class AppRegistration
{
    private readonly AppFactory appFactory;

    public AppRegistration(AppFactory appFactory)
    {
        this.appFactory = appFactory;
    }

    public async Task<AppWithModKeyModel> Run(AppApiTemplateModel template, AppVersionKey versionKey)
    {
        var appKey = template.AppKey;
        var app = await appFactory.Apps.App(appKey);
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
        var modifier = await addAppModifier(appKey, app);
        await addManageCacheRoleToHubSystemUsers(app);
        return new AppWithModKeyModel(app.ToAppModel(), modifier.ModKey().Value);
    }

    private static async Task updateResourceGroupFromTemplate(App app, AppVersion appVersion, AppApiGroupTemplateModel groupTemplate)
    {
        var modCategoryName = new ModifierCategoryName(groupTemplate.ModCategory);
        var modCategory = await app.AddModCategoryIfNotFound(modCategoryName);
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

    private async Task<Modifier> addAppModifier(AppKey appKey, App app)
    {
        var hubApp = await appFactory.Apps.App(HubInfo.AppKey);
        var appModCategory = await hubApp.ModCategory(HubInfo.ModCategories.Apps);
        var appModifier = await appModCategory.AddOrUpdateModifier(app.ID.Value, $"{appKey.Name.DisplayText} {appKey.Type.DisplayText}");
        var systemUsers = await appFactory.SystemUsers.SystemUsers(appKey);
        var hubAdminRole = await hubApp.Role(AppRoleName.Admin);
        foreach (var systemUser in systemUsers)
        {
            await systemUser.Modifier(appModifier).AssignRole(hubAdminRole);
        }
        return appModifier;
    }

    private async Task addManageCacheRoleToHubSystemUsers(App app)
    {
        var hubSystemUsers = await appFactory.SystemUsers.SystemUsers(HubInfo.AppKey);
        var manageCacheRole = await app.AddRoleIfNotFound(AppRoleName.ManageUserCache);
        foreach (var hubSystemUser in hubSystemUsers)
        {
            await hubSystemUser.AssignRole(manageCacheRole);
        }
    }
}