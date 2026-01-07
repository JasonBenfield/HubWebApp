using XTI_App.Abstractions;
using XTI_App.Api;

namespace XTI_Hub;

public sealed class EfAppContext : ISourceAppContext
{
    private readonly HubFactory hubFactory;
    private readonly AppKey appKey;
    private readonly AppVersionKey defaultVersionKey;

    public EfAppContext(HubFactory hubFactory, AppKey appKey, AppVersionKey defaultVersionKey)
    {
        this.hubFactory = hubFactory;
        this.appKey = appKey;
        this.defaultVersionKey = defaultVersionKey;
    }

    public Task<AppContextModel> App(CancellationToken ct) => App(defaultVersionKey, ct);

    public async Task<AppContextModel> App(AppVersionKey versionKey, CancellationToken ct)
    {
        var app = await hubFactory.Apps.AppOrUnknown(appKey, ct);
        var appVersion = await app.Version(versionKey);
        var appContextModel = await App(appVersion, ct);
        return appContextModel;
    }

    public async Task<AppContextModel> App(AppVersion appVersion, CancellationToken ct)
    {
        var roles = await appVersion.App.Roles(ct);
        var roleModels = roles.Select(r => r.ToModel()).ToArray();
        var modCategories = await appVersion.App.ModCategories(ct);
        var resourceGroups = await appVersion.ResourceGroups();
        var resourceGroupModels = new List<AppContextResourceGroupModel>();
        foreach (var resourceGroup in resourceGroups)
        {
            var resources = await resourceGroup.Resources();
            var resourceModels = new List<AppContextResourceModel>();
            foreach (var resource in resources)
            {
                var allowedResourceRoles = await resource.AllowedRoles(ct);
                resourceModels.Add
                (
                    new AppContextResourceModel
                    (
                        resource.ToModel(),
                        allowedResourceRoles.Select(r => roleModels.First(rm => r.ID == rm.ID)).ToArray()
                    )
                );
            }
            var allowedGroupRoles = await resourceGroup.AllowedRoles(ct);
            resourceGroupModels.Add
            (
                new AppContextResourceGroupModel
                (
                    resourceGroup.ToModel(),
                    resourceModels.ToArray(),
                    allowedGroupRoles.Select(r => roleModels.First(rm => r.ID == rm.ID)).ToArray()

                )
            );
        }
        var defaultModifier = await appVersion.App.DefaultModifier(ct);
        return new AppContextModel
        (
            appVersion.App.ToModel(),
            appVersion.Version.ToModel(),
            roleModels,
            modCategories.Select(mc => mc.ToModel()).ToArray(),
            resourceGroupModels.ToArray(),
            defaultModifier.ToModel()
        );
    }

    public async Task<ModifierModel> Modifier(ModifierCategoryModel category, ModifierKey modKey, CancellationToken ct)
    {
        var app = await hubFactory.Apps.AppOrUnknown(appKey, ct);
        var modCategory = await app.ModCategory(category.ID, ct);
        var modifier = await modCategory.ModifierByModKey(modKey, ct);
        return modifier.ToModel();
    }
}