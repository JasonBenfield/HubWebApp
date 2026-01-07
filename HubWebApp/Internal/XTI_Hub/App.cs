using System.Security.Cryptography;
using XTI_App.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class App
{
    private readonly HubFactory factory;
    private readonly AppEntity app;

    internal App(HubFactory factory, AppEntity app)
    {
        this.factory = factory;
        this.app = app ?? new AppEntity();
        ID = this.app.ID;
    }

    internal int ID { get; }

    public string SerializedDefaultOptions { get => app.SerializedDefaultOptions; }

    public bool AppKeyEquals(AppKey appKey) => appKey.Equals(GetAppKey());

    internal Task<ModifierCategory> AddOrUpdateModCategory(ModifierCategoryName name, CancellationToken ct) =>
        factory.ModCategories.AddOrUpdate(this, name, ct);

    internal Task<Modifier[]> Modifiers(CancellationToken ct) => factory.Modifiers.ModifiersForApp(this, ct);

    public Task<Modifier> Modifier(int modifierID, CancellationToken ct)
        => factory.Modifiers.ModifierForApp(this, modifierID, ct);

    public async Task<Modifier> DefaultModifier(CancellationToken ct)
    {
        var modCategory = await ModCategory(ModifierCategoryName.Default, ct);
        var modifier = await modCategory.ModifierByModKey(ModifierKey.Default, ct);
        return modifier;
    }

    public Task<ModifierCategory[]> ModCategories(CancellationToken ct) => factory.ModCategories.Categories(this, ct);

    public Task<ModifierCategory> ModCategory(int modCategoryID, CancellationToken ct) => factory.ModCategories.Category(this, modCategoryID, ct);

    public Task<ModifierCategory> ModCategory(ModifierCategoryName name, CancellationToken ct) => factory.ModCategories.Category(this, name, ct);

    public Task<AppRole> AddOrUpdateRole(AppRoleName name, CancellationToken ct) => factory.Roles.AddOrUpdate(this, name, ct);

    public async Task<AppRole[]> Roles(CancellationToken ct)
    {
        var roles = await factory.Roles.RolesForApp(this, ct);
        return roles
            .Where(r => !r.IsDeactivated())
            .ToArray();
    }

    public async Task<AppRole[]> Roles(AppRoleName[] roleNames, CancellationToken ct)
    {
        var roles = await factory.Roles.RolesForApp(this, roleNames, ct);
        return roles
            .Where(r => !r.IsDeactivated())
            .ToArray();
    }

    public Task<AppRole> Role(int roleID, CancellationToken ct) =>
        factory.Roles.Role(this, roleID, ct);

    public Task<AppRole> Role(AppRoleName roleName, CancellationToken ct) =>
        factory.Roles.Role(this, roleName, ct);

    internal async Task<AppVersion> AddVersionIfNotFound(AppVersionKey versionKey)
    {
        var version = await factory.Versions.VersionByName(new AppVersionName(app.VersionName), versionKey);
        await AddVersionIfNotFound(version);
        return new AppVersion(factory, this, version);
    }

    internal Task AddVersionIfNotFound(XtiVersion version) => factory.Versions.AddVersionToAppIfNotFound(this, version);

    public Task<AppVersion> CurrentVersion() => factory.Versions.VersionByApp(this, AppVersionKey.Current);

    public async Task SetRoles(IEnumerable<AppRoleName> roleNames, CancellationToken ct)
    {
        var existingRoles = await factory.Roles.RolesForApp(this, ct);
        await factory.DB.Apps.Transaction(async () =>
        {
            await addRoles(roleNames, existingRoles, ct);
            var rolesToDelete = existingRoles
                .Where(r => !r.IsDeactivated() && !roleNames.Any(rn => r.NameEquals(rn)))
                .ToArray();
            await deleteRoles(rolesToDelete, ct);
        });
    }

    public Task UpdateDefaultOptions(string serializedDefaultOptions, CancellationToken ct) =>
        factory.DB.Apps.Update
        (
            app,
            a =>
            {
                a.SerializedDefaultOptions = serializedDefaultOptions;
            },
            ct
        );

    private async Task addRoles(IEnumerable<AppRoleName> roleNames, IEnumerable<AppRole> existingRoles, CancellationToken ct)
    {
        foreach (var roleName in roleNames)
        {
            var existingRole = existingRoles.FirstOrDefault(r => r.NameEquals(roleName));
            if (existingRole == null)
            {
                await AddOrUpdateRole(roleName, ct);
            }
            else if (existingRole.IsDeactivated())
            {
                await existingRole.Activate(ct);
            }
        }
    }

    private static async Task deleteRoles(IEnumerable<AppRole> rolesToDelete, CancellationToken ct)
    {
        foreach (var role in rolesToDelete)
        {
            await role.Deactivate(DateTimeOffset.Now, ct);
        }
    }

    public Task<AppVersion> Version(AppVersionKey versionKey) => factory.Versions.VersionByApp(this, versionKey);

    public Task<AppVersion> VersionOrDefault(AppVersionKey versionKey, CancellationToken ct) =>
        factory.Versions.VersionByAppOrUnknown(this, versionKey, ct);

    public Task<XtiVersion[]> Versions() => factory.Versions.VersionsByApp(this);

    public async Task<AppRequestExpandedModel[]> MostRecentRequests(int howMany)
    {
        var version = await CurrentVersion();
        var requests = await version.MostRecentRequests(howMany);
        return requests;
    }

    public async Task<LogEntry[]> MostRecentErrorLogEntries(int howMany)
    {
        var version = await CurrentVersion();
        var requests = await version.MostRecentLoggedErrors(howMany);
        return requests;
    }

    public AppModel ToModel()
    {
        var key = GetAppKey();
        return new
        (
            ID: ID,
            AppKey: key,
            VersionName: new AppVersionName(app.VersionName),
            PublicKey: key.IsAnyAppType(AppType.Values.Package, AppType.Values.WebPackage)
                ? ModifierKey.Default
                : new ModifierKey(key.Format())
        );
    }

    public override string ToString() => $"{nameof(App)} {ID}: {GetAppKey().Format()}";

    public AppKey GetAppKey() =>
        new AppKey(new AppName(app.DisplayText), AppType.Values.Value(app.Type));
}