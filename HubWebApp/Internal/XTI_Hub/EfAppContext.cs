using XTI_App.Abstractions;
using XTI_App.Api;

namespace XTI_Hub;

public sealed class EfAppContext : ISourceAppContext
{
    private readonly HubFactory hubFactory;
    private readonly AppKey defaultAppKey;
    private readonly AppVersionKey defaultVersionKey;

    public EfAppContext(HubFactory hubFactory)
        : this(hubFactory, AppKey.Unknown, AppVersionKey.None)
    {
    }

    public EfAppContext(HubFactory hubFactory, AppKey defaultAppKey, AppVersionKey defaultVersionKey)
    {
        this.hubFactory = hubFactory;
        this.defaultAppKey = defaultAppKey;
        this.defaultVersionKey = defaultVersionKey;
    }

    public Task<AppContextModel> App() => App(defaultVersionKey);

    public async Task<AppContextModel> App(AppVersionKey versionKey)
    {
        var app = await hubFactory.Apps.AppOrUnknown(defaultAppKey);
        var appVersion = await app.Version(versionKey);
        var appContextModel = await App(appVersion);
        return appContextModel;
    }

    public async Task<AppContextModel> App(AppVersion appVersion)
    {
        var roles = await appVersion.App.Roles();
        var roleModels = roles.Select(r => r.ToModel()).ToArray();
        var modCategories = await appVersion.App.ModCategories();
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
            appVersion.App.ToModel(),
            appVersion.Version.ToModel(),
            roleModels,
            modCategoryModels.ToArray(),
            resourceGroupModels.ToArray()
        );
    }
}