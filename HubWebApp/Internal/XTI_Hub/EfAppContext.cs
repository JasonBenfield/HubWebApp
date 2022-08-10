using XTI_App.Abstractions;
using XTI_App.Api;

namespace XTI_Hub;

public sealed class EfAppContext : ISourceAppContext
{
    private readonly HubFactory hubFactory;
    private readonly AppKey appKey;
    private readonly AppVersionKey versionKey;

    public EfAppContext(HubFactory hubFactory, AppKey appKey, AppVersionKey versionKey)
    {
        this.hubFactory = hubFactory;
        this.appKey = appKey;
        this.versionKey = versionKey;
    }

    public async Task<AppContextModel> App()
    {
        var app = await hubFactory.Apps.AppOrUnknown(appKey);
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