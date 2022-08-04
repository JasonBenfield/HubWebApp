using XTI_App.Abstractions;
using XTI_App.Api;

namespace XTI_Hub;

public sealed class EfAppContext : ISourceAppContext
{
    private readonly HubFactory appFactory;
    private readonly AppKey currentAppKey;
    private readonly AppVersionKey versionKey;

    public EfAppContext(HubFactory appFactory, AppKey currentAppKey, AppVersionKey versionKey)
    {
        this.appFactory = appFactory;
        this.currentAppKey = currentAppKey;
        this.versionKey = versionKey;
    }

    public Task<AppContextModel> App() => App(currentAppKey);

    public async Task<AppContextModel> App(AppKey appKey)
    {
        var app = await appFactory.Apps.AppOrUnknown(appKey);
        var appVersion = await app.Version(versionKey);
        var roles = await app.Roles();
        var roleModels = roles.Select(r => r.ToModel()).ToArray();
        var modCategories = await app.ModCategories();
        var modCategoryModels = new List<AppContextModifierCategoryModel>();
        foreach (var modCategory in modCategories)
        {
            var modifiers = await modCategory.Modifiers();
            modCategoryModels.Add
            (
                new AppContextModifierCategoryModel
                (
                    modCategory.ToModel(),
                    modifiers.Select(m => m.ToModel()).ToArray()
                )
            );
        }
        var resourceGroups = await appVersion.ResourceGroups();
        var resourceGroupModels = new List<AppContextResourceGroupModel>();
        foreach (var resourceGroup in resourceGroups)
        {
            var resources = await resourceGroup.Resources();
            var resourceModels = new List<AppContextResourceModel>();
            foreach (var resource in resources)
            {
                var allowedResourceRoles = await resource.AllowedRoles();
                resourceModels.Add
                (
                    new AppContextResourceModel
                    (
                        resource.ToModel(),
                        allowedResourceRoles.Select(r => roleModels.First(rm => r.ID == rm.ID)).ToArray()
                    )
                );
            }
            var allowedGroupRoles = await resourceGroup.AllowedRoles();
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
        return new AppContextModel
        (
            app.ToModel(),
            appVersion.ToVersionModel(),
            roleModels,
            modCategoryModels.ToArray(),
            resourceGroupModels.ToArray()
        );
    }
}