using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;
using XTI_Hub.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class App : IApp
{
    private readonly AppFactory factory;
    private readonly AppEntity record;

    internal App(AppFactory factory, AppEntity record)
    {
        this.factory = factory;
        this.record = record ?? new AppEntity();
        ID = new EntityID(this.record.ID);
    }

    public EntityID ID { get; }
    public AppKey Key() => new AppKey(record.Name, AppType.Values.Value(record.Type));
    public string Title { get => record.Title; }
    internal string Domain { get => record.Domain; }

    public async Task RegisterAsAuthenticator()
    {
        var authenticator = await factory.DB
            .Authenticators.Retrieve()
            .Where(auth => auth.AppID == ID.Value)
            .FirstOrDefaultAsync();
        if (authenticator == null)
        {
            authenticator = new AuthenticatorEntity
            {
                AppID = ID.Value
            };
            await factory.DB.Authenticators.Create(authenticator);
        }
    }

    public async Task<ModifierKey> ModKeyInHubApps()
    {
        var hubApp = await factory.Apps.App(HubInfo.AppKey);
        var modCategory = await hubApp.ModCategory(HubInfo.ModCategories.Apps);
        var modifier = await modCategory.ModifierByTargetID(ID.Value);
        return modifier.ModKey();
    }

    public Task<ModifierCategory> AddModCategoryIfNotFound(ModifierCategoryName name) =>
        factory.ModCategories.AddIfNotFound(this, name);

    public Task<Modifier> Modifier(int modifierID)
        => factory.Modifiers.ModifierForApp(this, modifierID);

    public async Task<Modifier> DefaultModifier()
    {
        var modCategory = await ModCategory(ModifierCategoryName.Default);
        var modifier = await modCategory.ModifierByModKey(ModifierKey.Default);
        return modifier;
    }

    public Task<ModifierCategory[]> ModCategories()
        => factory.ModCategories.Categories(this);

    public Task<ModifierCategory> ModCategory(int modCategoryID)
        => factory.ModCategories.Category(this, modCategoryID);

    async Task<IModifierCategory> IApp.ModCategory(ModifierCategoryName name)
        => await ModCategory(name);

    public Task<ModifierCategory> ModCategory(ModifierCategoryName name)
        => factory.ModCategories.Category(this, name);

    public Task<AppRole> AddRoleIfNotFound(AppRoleName name) =>
        factory.Roles.AddIfNotFound(this, name);

    async Task<IAppRole[]> IApp.Roles() => await Roles();

    public async Task<AppRole[]> Roles()
    {
        var roles = await factory.Roles.RolesForApp(this);
        return roles
            .Where(r => !r.IsDeactivated())
            .ToArray();
    }

    public Task<AppRole> Role(int roleID) =>
        factory.Roles.Role(this, roleID);

    public Task<AppRole> Role(AppRoleName roleName) =>
        factory.Roles.Role(this, roleName);

    public Task<AppVersion> AddVersionIfNotFound
    (
        string groupName,
        AppVersionKey key,
        DateTimeOffset timeAdded,
        AppVersionStatus status,
        AppVersionType type,
        AppVersionNumber versionNumber
    ) =>
        factory.Versions.AddIfNotFound(groupName, key, timeAdded, status, type, versionNumber, this);

    internal Task AddVersionIfNotFound(XtiVersion version) => factory.Versions.AddVersionToAppIfNotFound(this, version);

    public Task<AppVersion> CurrentVersion() => factory.Versions.VersionByApp(this, AppVersionKey.Current);

    public async Task SetRoles(IEnumerable<AppRoleName> roleNames)
    {
        var existingRoles = await factory.Roles.RolesForApp(this);
        await factory.DB.Apps.Transaction(async () =>
        {
            await addRoles(roleNames, existingRoles);
            var rolesToDelete = existingRoles
                .Where(r => !r.IsDeactivated() && !roleNames.Any(rn => r.Name().Equals(rn)))
                .ToArray();
            await deleteRoles(rolesToDelete);
        });
    }

    private async Task addRoles(IEnumerable<AppRoleName> roleNames, IEnumerable<AppRole> existingRoles)
    {
        foreach (var roleName in roleNames)
        {
            var existingRole = existingRoles.FirstOrDefault(r => r.Name().Equals(roleName));
            if (existingRole == null)
            {
                await AddRoleIfNotFound(roleName);
            }
            else if (existingRole.IsDeactivated())
            {
                await existingRole.Activate();
            }
        }
    }

    private static async Task deleteRoles(IEnumerable<AppRole> rolesToDelete)
    {
        foreach (var role in rolesToDelete)
        {
            await role.Deactivate(DateTimeOffset.Now);
        }
    }

    async Task<IAppVersion> IApp.Version(AppVersionKey versionKey) => await Version(versionKey);

    public Task<AppVersion> Version(AppVersionKey versionKey) => factory.Versions.VersionByApp(this, versionKey);

    public Task<AppVersion> VersionOrDefault(AppVersionKey versionKey) =>
        factory.Versions.VersionByAppOrDefault(this, versionKey);

    public Task<XtiVersion[]> Versions() => factory.Versions.VersionsByApp(this);

    public async Task<AppRequestExpandedModel[]> MostRecentRequests(int howMany)
    {
        var version = await CurrentVersion();
        var requests = await version.MostRecentRequests(howMany);
        return requests;
    }

    public async Task<AppEvent[]> MostRecentErrorEvents(int howMany)
    {
        var version = await CurrentVersion();
        var requests = await version.MostRecentErrorEvents(howMany);
        return requests;
    }

    public AppModel ToAppModel()
    {
        var key = Key();
        return new AppModel
        {
            ID = ID.Value,
            AppName = key.Name.DisplayText,
            Title = record.Title,
            Type = key.Type,
            Domain = record.Domain
        };
    }

    public override string ToString() => $"{nameof(App)} {ID.Value}: {record.Name}";

}