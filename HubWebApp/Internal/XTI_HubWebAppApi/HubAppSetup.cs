namespace XTI_HubWebAppApi;

public sealed class HubAppSetup : IAppSetup
{
    private readonly EfHubDB db;
    private readonly HubAppApiFactory apiFactory;

    public HubAppSetup(EfHubDB db, HubAppApiFactory apiFactory)
    {
        this.db = db;
        this.apiFactory = apiFactory;
    }

    public Task Run(AppVersionKey versionKey, CancellationToken ct) =>
        db.Transaction(() => RegisterApp(versionKey, ct));

    private async Task RegisterApp(AppVersionKey versionKey, CancellationToken ct)
    {
        var template = apiFactory.CreateTemplate();
        var registration = new AppRegistration(db);
        await registration.Run(template.ToModel(), versionKey, ct);
        await AddAppModifiers(ct);
        await AddUserGroupModifiers(ct);
    }

    private async Task AddAppModifiers(CancellationToken ct)
    {
        var efHubApp = await db.Apps.App(HubInfo.AppKey, ct);
        var efAppModCategory = await efHubApp.ModCategory(HubInfo.ModCategories.Apps, ct);
        var efApps = await db.Apps.All(ct);
        var apps = efApps
            .Select(a => a.ToModel())
            .Where
            (
                a => !a.AppKey.Equals(HubInfo.AppKey) &&
                    !a.AppKey.IsAnyAppType(AppType.Values.NotFound, AppType.Values.Package, AppType.Values.WebPackage)
            )
            .ToArray();
        foreach (var app in apps)
        {
            await efAppModCategory.AddOrUpdateModifier
            (
                app.PublicKey,
                app.ID,
                app.AppKey.Format(),
                ct
            );
        }
    }

    private async Task AddUserGroupModifiers(CancellationToken ct)
    {
        var efHubApp = await db.Apps.App(HubInfo.AppKey, ct);
        var efUserGroupsModCategory = await efHubApp.ModCategory(HubInfo.ModCategories.UserGroups, ct);
        var efUserGroups = await db.UserGroups.UserGroups(ct);
        foreach (var efUserGroup in efUserGroups)
        {
            var userGroup = efUserGroup.ToModel();
            await efUserGroupsModCategory.AddOrUpdateModifier
            (
                userGroup.PublicKey,
                userGroup.ID,
                userGroup.GroupName.DisplayText,
                ct
            );
        }
    }

}