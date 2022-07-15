using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;
using XTI_Hub.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class App
{
    private readonly HubFactory factory;
    private readonly AppEntity record;

    internal App(HubFactory factory, AppEntity record)
    {
        this.factory = factory;
        this.record = record ?? new AppEntity();
        ID = this.record.ID;
    }

    public int ID { get; }
    public AppKey Key() => new AppKey(record.Name, AppType.Values.Value(record.Type));
    public string Title { get => record.Title; }

    public async Task RegisterAsAuthenticator()
    {
        var authenticator = await factory.DB
            .Authenticators.Retrieve()
            .Where(auth => auth.AppID == ID)
            .FirstOrDefaultAsync();
        if (authenticator == null)
        {
            authenticator = new AuthenticatorEntity
            {
                AppID = ID
            };
            await factory.DB.Authenticators.Create(authenticator);
        }
    }

    internal Task<ModifierCategory> AddModCategoryIfNotFound(ModifierCategoryName name) =>
        factory.ModCategories.AddIfNotFound(this, name);

    internal Task<Modifier[]> Modifiers() => factory.Modifiers.ModifiersForApp(this);

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

    public Task<ModifierCategory> ModCategory(ModifierCategoryName name)
        => factory.ModCategories.Category(this, name);

    public Task<AppRole> AddRoleIfNotFound(AppRoleName name) =>
        factory.Roles.AddIfNotFound(this, name);

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

    public Task<AppVersion> Version(AppVersionKey versionKey) => factory.Versions.VersionByApp(this, versionKey);

    public Task<AppVersion> VersionOrDefault(AppVersionKey versionKey) =>
        factory.Versions.VersionByAppOrUnknown(this, versionKey);

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

    public AppModel ToAppModel()
    {
        var key = Key();
        return new AppModel
        (
            ID: ID,
            AppKey: key,
            VersionName: new AppVersionName(record.VersionName),
            Title: record.Title,
            PublicKey: new ModifierKey(key.Format())
        );
    }

    public override string ToString() => $"{nameof(App)} {ID}: {Key().Format()}";

}