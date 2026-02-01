using XTI_App.Abstractions;

namespace XTI_Hub;

public sealed class AppRegistration
{
    private readonly EfHubDB db;

    public AppRegistration(EfHubDB db)
    {
        this.db = db;
    }

    public async Task<AppModel> Run(AppApiTemplateModel template, AppVersionKey versionKey, CancellationToken ct)
    {
        var appKey = template.AppKey;
        var efApp = await db.Apps.App(appKey, ct);
        await efApp.UpdateDefaultOptions(template.SerializedDefaultOptions, ct);
        var roleNames = template.RecursiveRoles()
            .Union([AppRoleName.DenyAccess])
            .Distinct();
        await efApp.SetRoles(roleNames, ct);
        var efVersion = await efApp.AddVersionIfNotFound(versionKey, ct);
        foreach (var groupTemplate in template.GroupTemplates)
        {
            await UpdateResourceGroupFromTemplate(efApp, efVersion, groupTemplate, ct);
        }
        await AddAppModifier(appKey, efApp, ct);
        await AddManageCacheRoleToHubSystemUsers(efApp, ct);
        return efApp.ToModel();
    }

    private static async Task UpdateResourceGroupFromTemplate(EfApp efApp, EfAppVersion efAppVersion, AppApiGroupTemplateModel groupTemplate, CancellationToken ct)
    {
        var efModCategory = await efApp.AddOrUpdateModCategory(groupTemplate.ModCategory, ct);
        var efResourceGroup = await efAppVersion.AddOrUpdateResourceGroup(groupTemplate.Name, efModCategory, ct);
        if (groupTemplate.IsAnonymousAllowed)
        {
            await efResourceGroup.AllowAnonymous(ct);
        }
        else
        {
            await efResourceGroup.DenyAnonymous(ct);
        }
        var allowedGroupRoles = await RolesFromNames(efApp, groupTemplate.Roles, ct);
        await efResourceGroup.SetRoleAccess(allowedGroupRoles, ct);
        foreach (var actionTemplate in groupTemplate.ActionTemplates)
        {
            await UpdateResourceFromTemplate(efApp, efResourceGroup, actionTemplate, ct);
        }
    }

    private static async Task UpdateResourceFromTemplate(EfApp app, EfResourceGroup resourceGroup, AppApiActionTemplateModel actionTemplate, CancellationToken ct)
    {
        var efResource = await resourceGroup.AddOrUpdateResource(actionTemplate.Name, actionTemplate.ResultType, ct);
        if (actionTemplate.IsAnonymousAllowed)
        {
            await efResource.AllowAnonymous(ct);
        }
        else
        {
            await efResource.DenyAnonymous(ct);
        }
        var efAllowedResourceRoles = await RolesFromNames(app, actionTemplate.Roles, ct);
        await efResource.SetRoleAccess(efAllowedResourceRoles, ct);
    }

    private static async Task<EfAppRole[]> RolesFromNames(EfApp app, IEnumerable<AppRoleName> roleNames, CancellationToken ct)
    {
        var efRoles = new List<EfAppRole>();
        foreach (var roleName in roleNames)
        {
            var efRole = await app.Role(roleName, ct);
            efRoles.Add(efRole);
        }
        return efRoles.ToArray();
    }

    private async Task<EfModifier> AddAppModifier(AppKey appKey, EfApp app, CancellationToken ct)
    {
        var hubApp = await db.Apps.App(HubInfo.AppKey, ct);
        var appModCategory = await hubApp.ModCategory(HubInfo.ModCategories.Apps, ct);
        var appModel = app.ToModel();
        var appModifier = await appModCategory.AddOrUpdateModifier
        (
            appModel.PublicKey,
            appModel.ID,
            appKey.Format(),
            ct
        );
        var systemUsers = await db.SystemUsers.SystemUsers(appKey, ct);
        var hubAdminRole = await hubApp.Role(AppRoleName.Admin, ct);
        foreach (var systemUser in systemUsers)
        {
            await systemUser.Modifier(appModifier).AssignRole(hubAdminRole, ct);
        }
        return appModifier;
    }

    private async Task AddManageCacheRoleToHubSystemUsers(EfApp app, CancellationToken ct)
    {
        var hubSystemUsers = await db.SystemUsers.SystemUsers(HubInfo.AppKey, ct);
        var manageCacheRole = await app.AddOrUpdateRole(AppRoleName.ManageUserCache, ct);
        foreach (var hubSystemUser in hubSystemUsers)
        {
            await hubSystemUser.AssignRole(manageCacheRole, ct);
        }
    }
}