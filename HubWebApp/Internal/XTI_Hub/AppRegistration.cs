using XTI_App.Abstractions;

namespace XTI_Hub;

public sealed class AppRegistration
{
    private readonly HubFactory hubFactory;

    public AppRegistration(HubFactory hubFactory)
    {
        this.hubFactory = hubFactory;
    }

    public async Task<AppModel> Run(AppApiTemplateModel template, AppVersionKey versionKey, CancellationToken ct)
    {
        var appKey = template.AppKey;
        var app = await hubFactory.Apps.App(appKey, ct);
        await app.UpdateDefaultOptions(template.SerializedDefaultOptions, ct);
        var roleNames = template.RecursiveRoles()
            .Union([AppRoleName.DenyAccess])
            .Distinct();
        await app.SetRoles(roleNames, ct);
        var version = await app.AddVersionIfNotFound(versionKey, ct);
        foreach (var groupTemplate in template.GroupTemplates)
        {
            await UpdateResourceGroupFromTemplate(app, version, groupTemplate, ct);
        }
        await AddAppModifier(appKey, app, ct);
        await AddManageCacheRoleToHubSystemUsers(app, ct);
        return app.ToModel();
    }

    private static async Task UpdateResourceGroupFromTemplate(App app, AppVersion appVersion, AppApiGroupTemplateModel groupTemplate, CancellationToken ct)
    {
        var modCategory = await app.AddOrUpdateModCategory(groupTemplate.ModCategory, ct);
        var resourceGroup = await appVersion.AddOrUpdateResourceGroup(groupTemplate.Name, modCategory, ct);
        if (groupTemplate.IsAnonymousAllowed)
        {
            await resourceGroup.AllowAnonymous(ct);
        }
        else
        {
            await resourceGroup.DenyAnonymous(ct);
        }
        var allowedGroupRoles = await RolesFromNames(app, groupTemplate.Roles, ct);
        await resourceGroup.SetRoleAccess(allowedGroupRoles, ct);
        foreach (var actionTemplate in groupTemplate.ActionTemplates)
        {
            await UpdateResourceFromTemplate(app, resourceGroup, actionTemplate, ct);
        }
    }

    private static async Task UpdateResourceFromTemplate(App app, ResourceGroup resourceGroup, AppApiActionTemplateModel actionTemplate, CancellationToken ct)
    {
        var resource = await resourceGroup.AddOrUpdateResource(actionTemplate.Name, actionTemplate.ResultType, ct);
        if (actionTemplate.IsAnonymousAllowed)
        {
            await resource.AllowAnonymous(ct);
        }
        else
        {
            await resource.DenyAnonymous(ct);
        }
        var allowedResourceRoles = await RolesFromNames(app, actionTemplate.Roles, ct);
        await resource.SetRoleAccess(allowedResourceRoles, ct);
    }

    private static async Task<IEnumerable<AppRole>> RolesFromNames(App app, IEnumerable<AppRoleName> roleNames, CancellationToken ct)
    {
        var roles = new List<AppRole>();
        foreach (var roleName in roleNames)
        {
            var role = await app.Role(roleName, ct);
            roles.Add(role);
        }
        return roles;
    }

    private async Task<Modifier> AddAppModifier(AppKey appKey, App app, CancellationToken ct)
    {
        var hubApp = await hubFactory.Apps.App(HubInfo.AppKey, ct);
        var appModCategory = await hubApp.ModCategory(HubInfo.ModCategories.Apps, ct);
        var appModel = app.ToModel();
        var appModifier = await appModCategory.AddOrUpdateModifier
        (
            appModel.PublicKey,
            appModel.ID,
            appKey.Format(),
            ct
        );
        var systemUsers = await hubFactory.SystemUsers.SystemUsers(appKey, ct);
        var hubAdminRole = await hubApp.Role(AppRoleName.Admin, ct);
        foreach (var systemUser in systemUsers)
        {
            await systemUser.Modifier(appModifier).AssignRole(hubAdminRole, ct);
        }
        return appModifier;
    }

    private async Task AddManageCacheRoleToHubSystemUsers(App app, CancellationToken ct)
    {
        var hubSystemUsers = await hubFactory.SystemUsers.SystemUsers(HubInfo.AppKey, ct);
        var manageCacheRole = await app.AddOrUpdateRole(AppRoleName.ManageUserCache, ct);
        foreach (var hubSystemUser in hubSystemUsers)
        {
            await hubSystemUser.AssignRole(manageCacheRole, ct);
        }
    }
}