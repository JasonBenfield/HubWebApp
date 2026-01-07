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

    public async Task Run(AppVersionKey versionKey, CancellationToken ct)
    {
        var template = apiFactory.CreateTemplate();
        var registration = new AppRegistration(hubFactory);
        await registration.Run(template.ToModel(), versionKey, ct);
        await AddAppModifiers(ct);
        await AddUserGroupModifiers(ct);
    }

    private async Task AddAppModifiers(CancellationToken ct)
    {
        var hubApp = await hubFactory.Apps.App(HubInfo.AppKey, ct);
        var appModCategory = await hubApp.ModCategory(HubInfo.ModCategories.Apps, ct);
        var apps = await hubFactory.Apps.All(ct);
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
                appModel.AppKey.Format(),
                ct
            );
        }
    }

    private async Task AddUserGroupModifiers(CancellationToken ct)
    {
        var hubApp = await hubFactory.Apps.App(HubInfo.AppKey, ct);
        var userGroupsModCategory = await hubApp.ModCategory(HubInfo.ModCategories.UserGroups, ct);
        var userGroups = await hubFactory.UserGroups.UserGroups(ct);
        foreach (var userGroup in userGroups)
        {
            var userGroupModel = userGroup.ToModel();
            await userGroupsModCategory.AddOrUpdateModifier
            (
                userGroupModel.PublicKey,
                userGroupModel.ID,
                userGroupModel.GroupName.DisplayText,
                ct
            );
        }
    }

}