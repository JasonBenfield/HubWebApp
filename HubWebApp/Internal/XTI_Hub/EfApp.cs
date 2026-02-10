using XTI_App.Abstractions;
using XTI_Hub.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class EfApp
{
    private readonly EfHubDB db;
    private readonly AppEntity app;

    internal EfApp(EfHubDB db, AppEntity app)
    {
        this.db = db;
        this.app = app ?? new AppEntity();
        ID = this.app.ID;
    }

    internal int ID { get; }

    public string SerializedDefaultOptions { get => app.SerializedDefaultOptions; }

    public bool AppKeyEquals(AppKey appKey) => appKey.Equals(GetAppKey());

    internal Task<EfModifierCategory> AddOrUpdateModCategory(ModifierCategoryName name, CancellationToken ct) =>
        db.ModCategories.AddOrUpdate(this, name, ct);

    internal Task<EfModifier[]> Modifiers(CancellationToken ct) => db.Modifiers.ModifiersForApp(this, ct);

    public Task<EfModifier> Modifier(int modifierID, CancellationToken ct)
        => db.Modifiers.ModifierForApp(this, modifierID, ct);

    public async Task<EfModifier> DefaultModifier(CancellationToken ct)
    {
        var efModCategory = await ModCategory(ModifierCategoryName.Default, ct);
        var efModifier = await efModCategory.ModifierByModKey(ModifierKey.Default, ct);
        return efModifier;
    }

    public Task<EfModifierCategory[]> ModCategories(CancellationToken ct) => db.ModCategories.Categories(this, ct);

    public Task<EfModifierCategory> ModCategory(int modCategoryID, CancellationToken ct) => db.ModCategories.Category(this, modCategoryID, ct);

    public Task<EfModifierCategory> ModCategory(ModifierCategoryName name, CancellationToken ct) => db.ModCategories.Category(this, name, ct);

    public Task<EfAppRole> AddOrUpdateRole(AppRoleName name, CancellationToken ct) => db.Roles.AddOrUpdate(this, name, ct);

    public async Task<EfAppRole[]> Roles(CancellationToken ct)
    {
        var efRoles = await db.Roles.RolesForApp(this, ct);
        return efRoles
            .Where(r => !r.IsDeactivated())
            .ToArray();
    }

    public async Task<EfAppRole[]> Roles(AppRoleName[] roleNames, CancellationToken ct)
    {
        var efRoles = await db.Roles.RolesForApp(this, roleNames, ct);
        return efRoles
            .Where(r => !r.IsDeactivated())
            .ToArray();
    }

    public Task<EfAppRole> Role(int roleID, CancellationToken ct) =>
        db.Roles.Role(this, roleID, ct);

    public Task<EfAppRole> Role(AppRoleName roleName, CancellationToken ct) =>
        db.Roles.Role(this, roleName, ct);

    internal async Task<EfAppVersion> AddVersionIfNotFound(AppVersionKey versionKey, CancellationToken ct)
    {
        var efVersion = await db.Versions.VersionByName(new AppVersionName(app.VersionName), versionKey, ct);
        await AddVersionIfNotFound(efVersion, ct);
        return new EfAppVersion(db, this, efVersion);
    }

    public Task AddVersionIfNotFound(EfVersion version, CancellationToken ct) => db.Versions.AddVersionToAppIfNotFound(this, version, ct);

    public Task<EfAppVersion> CurrentVersion(CancellationToken ct) => db.Versions.VersionByApp(this, AppVersionKey.Current, ct);

    public async Task SetRoles(IEnumerable<AppRoleName> roleNames, CancellationToken ct)
    {
        var efExistingRoles = await db.Roles.RolesForApp(this, ct);
        await AddRoles(roleNames, efExistingRoles, ct);
        var efRolesToDelete = efExistingRoles
            .Where(r => !r.IsDeactivated() && !roleNames.Any(rn => r.NameEquals(rn)))
            .ToArray();
        await DeleteRoles(efRolesToDelete, ct);
    }

    public Task UpdateDefaultOptions(string serializedDefaultOptions, CancellationToken ct) =>
        db.Context.Apps.Update
        (
            app,
            a =>
            {
                a.SerializedDefaultOptions = serializedDefaultOptions;
            },
            ct
        );

    private async Task AddRoles(IEnumerable<AppRoleName> roleNames, IEnumerable<EfAppRole> efExistingRoles, CancellationToken ct)
    {
        foreach (var roleName in roleNames)
        {
            var efExistingRole = efExistingRoles.FirstOrDefault(r => r.NameEquals(roleName));
            if (efExistingRole == null)
            {
                await AddOrUpdateRole(roleName, ct);
            }
            else if (efExistingRole.IsDeactivated())
            {
                await efExistingRole.Activate(ct);
            }
        }
    }

    private static async Task DeleteRoles(EfAppRole[] efRolesToDelete, CancellationToken ct)
    {
        foreach (var efRole in efRolesToDelete)
        {
            await efRole.Deactivate(DateTimeOffset.Now, ct);
        }
    }

    public Task<EfAppVersion> Version(AppVersionKey versionKey, CancellationToken ct) => db.Versions.VersionByApp(this, versionKey, ct);

    public Task<EfAppVersion> VersionOrDefault(AppVersionKey versionKey, CancellationToken ct) =>
        db.Versions.VersionByAppOrUnknown(this, versionKey, ct);

    public Task<EfVersion[]> Versions(CancellationToken ct) => db.Versions.VersionsByApp(this, ct);

    public async Task<AppRequestExpandedModel[]> MostRecentRequests(int howMany, CancellationToken ct)
    {
        var efVersion = await CurrentVersion(ct);
        var efRequests = await efVersion.MostRecentRequests(howMany, ct);
        return efRequests;
    }

    public async Task<EfLogEntry[]> MostRecentErrorLogEntries(int howMany, CancellationToken ct)
    {
        var efVersion = await CurrentVersion(ct);
        var efRequests = await efVersion.MostRecentLoggedErrors(howMany, ct);
        return efRequests;
    }

    public Task<EfAppCommand> AddCommand
    (
        EfInstallLocation efLocation,
        AppCommandName commandName,
        string serializedRequest,
        DateTimeOffset timeAdded,
        DateTimeOffset timeStarted,
        CancellationToken ct
    ) => db.AppCommands.Add(app, efLocation, commandName, serializedRequest, timeAdded, timeStarted, ct);

    public Task<EfAppCommand> Command(int commandID, CancellationToken ct) =>
        db.AppCommands.Command(app, commandID, ct);

    public AppModel ToModel()
    {
        var key = GetAppKey();
        return new
        (
            ID: ID,
            AppKey: key,
            VersionName: new AppVersionName(app.VersionName),
            RepoOwner: app.RepoOwner,
            RepoName: app.RepoName,
            PublicKey: key.IsAnyAppType(AppType.Values.Package, AppType.Values.WebPackage)
                ? ModifierKey.Default
                : new ModifierKey(key.Format())
        );
    }

    public override string ToString() => $"{nameof(EfApp)} {ID}: {GetAppKey().Format()}";

    public AppKey GetAppKey() =>
        new AppKey(new AppName(app.DisplayText), AppType.Values.Value(app.Type));

}