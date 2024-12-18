namespace XTI_HubWebAppApi;

public sealed class HubAppSetup : IAppSetup
{
    private readonly HubFactory hubFactory;
    private readonly HubAppApiFactory apiFactory;

    public HubAppSetup(HubFactory hubFactory, HubAppApiFactory apiFactory)
    {
        this.hubFactory = hubFactory;
        this.apiFactory = apiFactory;
    }

    public async Task Run(AppVersionKey versionKey)
    {
        var template = apiFactory.CreateTemplate();
        var registration = new AppRegistration(hubFactory);
        await registration.Run(template.ToModel(), versionKey);
        await AddAppModifiers();
        await AddUserGroupModifiers();
    }

    private async Task AddAppModifiers()
    {
        var hubApp = await hubFactory.Apps.App(HubInfo.AppKey);
        var appModCategory = await hubApp.ModCategory(HubInfo.ModCategories.Apps);
        var apps = await hubFactory.Apps.All();
        var appModels = apps
            .Select(a => a.ToModel())
            .Where
            (
                a => !a.AppKey.Equals(HubInfo.AppKey) &&
                    !a.AppKey.IsAnyAppType(AppType.Values.NotFound, AppType.Values.Package, AppType.Values.WebPackage)
            );
        foreach (var appModel in appModels)
        {
            await appModCategory.AddOrUpdateModifier
            (
                appModel.PublicKey,
                appModel.ID,
                appModel.AppKey.Format()
            );
        }
    }

    private async Task AddUserGroupModifiers()
    {
        var hubApp = await hubFactory.Apps.App(HubInfo.AppKey);
        var userGroupsModCategory = await hubApp.ModCategory(HubInfo.ModCategories.UserGroups);
        var userGroups = await hubFactory.UserGroups.UserGroups();
        foreach (var userGroup in userGroups)
        {
            var userGroupModel = userGroup.ToModel();
            await userGroupsModCategory.AddOrUpdateModifier
            (
                userGroupModel.PublicKey,
                userGroupModel.ID,
                userGroupModel.GroupName.DisplayText
            );
        }
    }

}