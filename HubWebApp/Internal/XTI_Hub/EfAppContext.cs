using XTI_App.Abstractions;
using XTI_App.Api;

namespace XTI_Hub;

public sealed class EfAppContext : ISourceAppContext
{
    private readonly EfHubDB db;
    private readonly AppKey appKey;
    private readonly AppVersionKey defaultVersionKey;

    public EfAppContext(EfHubDB db, AppKey appKey, AppVersionKey defaultVersionKey)
    {
        this.db = db;
        this.appKey = appKey;
        this.defaultVersionKey = defaultVersionKey;
    }

    public Task<AppContextModel> App(CancellationToken ct) => App(defaultVersionKey, ct);

    public async Task<AppContextModel> App(AppVersionKey versionKey, CancellationToken ct)
    {
        var efApp = await db.Apps.AppOrUnknown(appKey, ct);
        var efAppVersion = await efApp.Version(versionKey, ct);
        var appContext = await App(efAppVersion, ct);
        return appContext;
    }

    public async Task<AppContextModel> App(EfAppVersion appVersion, CancellationToken ct)
    {
        var efRoles = await appVersion.App.Roles(ct);
        var roles = efRoles.Select(r => r.ToModel()).ToArray();
        var efModCategories = await appVersion.App.ModCategories(ct);
        var efResourceGroups = await appVersion.ResourceGroups(ct);
        var resourceGroups = new List<AppContextResourceGroupModel>();
        foreach (var efResourceGroup in efResourceGroups)
        {
            var efResources = await efResourceGroup.Resources(ct);
            var resources = new List<AppContextResourceModel>();
            foreach (var efResource in efResources)
            {
                var efAllowedResourceRoles = await efResource.AllowedRoles(ct);
                resources.Add
                (
                    new AppContextResourceModel
                    (
                        efResource.ToModel(),
                        efAllowedResourceRoles.Select(r => roles.First(rm => r.ID == rm.ID)).ToArray()
                    )
                );
            }
            var efAllowedGroupRoles = await efResourceGroup.AllowedRoles(ct);
            resourceGroups.Add
            (
                new AppContextResourceGroupModel
                (
                    efResourceGroup.ToModel(),
                    resources.ToArray(),
                    efAllowedGroupRoles.Select(r => roles.First(rm => r.ID == rm.ID)).ToArray()

                )
            );
        }
        var efDefaultModifier = await appVersion.App.DefaultModifier(ct);
        return new AppContextModel
        (
            appVersion.App.ToModel(),
            appVersion.Version.ToModel(),
            roles,
            efModCategories.Select(mc => mc.ToModel()).ToArray(),
            resourceGroups.ToArray(),
            efDefaultModifier.ToModel()
        );
    }

    public async Task<ModifierModel> Modifier(ModifierCategoryModel category, ModifierKey modKey, CancellationToken ct)
    {
        var efApp = await db.Apps.AppOrUnknown(appKey, ct);
        var efModCategory = await efApp.ModCategory(category.ID, ct);
        var efModifier = await efModCategory.ModifierByModKey(modKey, ct);
        return efModifier.ToModel();
    }
}