using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;
using XTI_Hub.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class EfAppUser
{
    private readonly EfHubDB db;
    private readonly AppUserEntity user;

    internal EfAppUser(EfHubDB db, AppUserEntity user)
    {
        this.db = db;
        this.user = user ?? new AppUserEntity();
    }

    internal int ID { get => user.ID; }

    public bool HasID(int id) => ID == id;

    public bool IsUserName(AppUserName userName) => new AppUserName(user.UserName).Equals(userName);

    public bool IsPasswordCorrect(IHashedPassword hashedPassword) =>
        hashedPassword.Equals(user.Password);

    public async Task AssignRole(EfAppRole role, CancellationToken ct)
    {
        var app = await role.App(ct);
        var modifier = await app.DefaultModifier(ct);
        await Modifier(modifier).AssignRole(role, ct);
    }

    public async Task<AppUserModifier[]> Modifiers(EfApp app, CancellationToken ct)
    {
        var modifiers = await app.Modifiers(ct);
        var userModifiers = new List<AppUserModifier>();
        foreach (var modifier in modifiers)
        {
            userModifiers.Add(Modifier(modifier));
        }
        return userModifiers.ToArray();
    }

    public AppUserModifier Modifier(EfModifier modifier) =>
        new AppUserModifier(db, this, modifier);

    public Task ChangePassword(IHashedPassword password, CancellationToken ct) =>
        db.Context.Users.Update(user, u => u.Password = password.Value(), ct);

    public Task Deactivate(DateTimeOffset timeDeactivated, CancellationToken ct) =>
        db.Context.Users.Update(user, u => u.TimeDeactivated = timeDeactivated, ct);

    public Task Reactivate(CancellationToken ct) =>
        db.Context.Users.Update(user, u => u.TimeDeactivated = DateTimeOffset.MaxValue, ct);

    public Task Edit(PersonName name, EmailAddress email, CancellationToken ct)
    {
        if (name.IsBlank())
        {
            name = new PersonName(user.UserName);
        }
        return db.Context.Users.Update
        (
            user,
            u =>
            {
                u.Name = name.Value;
                u.Email = email.Value;
            },
            ct
        );
    }

    public async Task DeleteAuthenticator(AuthenticatorKey authenticatorKey, string externalUserKey, CancellationToken ct)
    {
        var authenticatorIDs = QueryAuthenticatorIDs(authenticatorKey);
        var userAuthenticator = await GetUserAuthenticator(authenticatorIDs, externalUserKey, ct);
        if (userAuthenticator != null)
        {
            await db.Context.UserAuthenticators.Delete(userAuthenticator, ct);
        }
    }

    private Task<UserAuthenticatorEntity?> GetUserAuthenticator(IQueryable<int> authenticatorIDs, string externalUserKey, CancellationToken ct) =>
        db.Context.UserAuthenticators.Retrieve()
            .Where
            (
                ua =>
                    authenticatorIDs.Contains(ua.AuthenticatorID)
                    && ua.UserID == ID
                    && ua.ExternalUserKey == externalUserKey
            )
            .FirstOrDefaultAsync(ct);

    public async Task<AuthenticatorModel> AddAuthenticator(AuthenticatorKey authenticatorKey, string externalUserKey, CancellationToken ct)
    {
        var authenticatorIDs = QueryAuthenticatorIDs(authenticatorKey);
        var userAuthenticator = await GetUserAuthenticator(authenticatorIDs, ct);
        int authenticatorID;
        if (userAuthenticator == null)
        {
            authenticatorID = await authenticatorIDs.FirstAsync();
            userAuthenticator = new UserAuthenticatorEntity
            {
                AuthenticatorID = authenticatorID,
                UserID = ID,
                ExternalUserKey = externalUserKey
            };
            await db.Context.UserAuthenticators.Create(userAuthenticator, ct);
        }
        else
        {
            authenticatorID = userAuthenticator.AuthenticatorID;
            await db.Context.UserAuthenticators.Update
            (
                userAuthenticator,
                ua => ua.ExternalUserKey = externalUserKey,
                ct
            );
        }
        return new AuthenticatorModel(authenticatorID, authenticatorKey);
    }

    private IQueryable<int> QueryAuthenticatorIDs(AuthenticatorKey authenticatorKey) =>
        db.Context.Authenticators.Retrieve()
            .Where(a => a.AuthenticatorKey == authenticatorKey.Value)
            .Select(a => a.ID);

    private Task<UserAuthenticatorEntity?> GetUserAuthenticator(IQueryable<int> authenticatorIDs, CancellationToken ct) =>
        db.Context.UserAuthenticators.Retrieve()
            .Where
            (
                ua =>
                    authenticatorIDs.Contains(ua.AuthenticatorID)
                    && ua.UserID == ID
            )
            .FirstOrDefaultAsync(ct);

    public async Task<UserAuthenticatorModel[]> Authenticators(CancellationToken ct)
    {
        var joinedEntities = await db.Context.UserAuthenticators.Retrieve()
            .Where
            (
                ua => ua.UserID == ID
            )
            .Join
            (
                db.Context.Authenticators.Retrieve(),
                ua => ua.AuthenticatorID,
                a => a.ID,
                (ua, a) => new
                {
                    AuthenticatorID = a.ID,
                    a.AuthenticatorKey,
                    a.AuthenticatorName,
                    ua.ExternalUserKey
                }
            )
            .ToArrayAsync(ct);
        return joinedEntities
            .Select
            (
                j => new UserAuthenticatorModel
                (
                    new AuthenticatorModel
                    (
                        j.AuthenticatorID,
                        new AuthenticatorKey(j.AuthenticatorName)
                    ),
                    j.ExternalUserKey
                )
            )
            .ToArray();
    }

    public async Task<AppPermission[]> GetAppPermissions(CancellationToken ct)
    {
        var efApps = await db.Apps.All(ct);
        var efHubApp = efApps.First(a => a.AppKeyEquals(HubInfo.AppKey));
        var efAppsModCategory = await efHubApp.ModCategory(HubInfo.ModCategories.Apps, ct);
        var appPermissions = new List<AppPermission>();
        var appsWithPermissions = efApps
            .Where
            (
                a => !a.ToModel().AppKey.IsAnyAppType
                (
                    AppType.Values.NotFound,
                    AppType.Values.Package,
                    AppType.Values.WebPackage
                )
            );
        foreach (var app in appsWithPermissions)
        {
            var permission = await GetAppPermission(efHubApp, efAppsModCategory, app, ct);
            appPermissions.Add(permission);
        }
        return appPermissions.ToArray();
    }

    public async Task<AppPermission> GetAppPermission(EfApp app, CancellationToken ct)
    {
        var efHubApp = await db.Apps.App(HubInfo.AppKey, ct);
        var efAppsModCategory = await efHubApp.ModCategory(HubInfo.ModCategories.Apps, ct);
        var permission = await GetAppPermission(efHubApp, efAppsModCategory, app, ct);
        return permission;
    }

    private async Task<AppPermission> GetAppPermission(EfApp efHubApp, EfModifierCategory efAppsModCategory, EfApp efApp, CancellationToken ct)
    {
        var app = efApp.ToModel();
        EfModifier modifier;
        if
        (
            app.AppKey.IsUnknown() ||
            app.AppKey.IsAnyAppType(AppType.Values.Package, AppType.Values.WebPackage)
        )
        {
            modifier = await efHubApp.DefaultModifier(ct);
        }
        else
        {
            modifier = await efAppsModCategory.ModifierByTargetID(app.ID, ct);
        }
        var efUserRoles = await Modifier(modifier).AssignedRoles(ct);
        var userRoles = efUserRoles.Select(ur => ur.ToModel());
        AppPermission permission;
        if (userRoles.Any(ur => ur.Name.Equals(AppRoleName.DenyAccess)))
        {
            permission = new AppPermission(efApp, false, false);
        }
        else
        {
            permission = new AppPermission
            (
                EfApp: efApp,
                CanView: userRoles
                    .Any(ur => ur.Name.EqualsAny(HubInfo.Roles.AppViewerRoles)),
                CanEdit: userRoles
                    .Any(ur => ur.Name.EqualsAny(HubInfo.Roles.AppEditorRoles))
            );
        }
        return permission;
    }

    public Task<EfAppUserGroup> UserGroup(CancellationToken ct) => db.UserGroups.UserGroup(user.GroupID, ct);

    public Task EditUserGroup(EfAppUserGroup userGroup, CancellationToken ct) =>
        db.Context.Users.Update
        (
            user,
            u =>
            {
                u.GroupID = userGroup.ID;
            },
            ct
        );

    public async Task<AppUserGroupPermission[]> GetUserGroupPermissions(CancellationToken ct)
    {
        var efUserGroups = await db.UserGroups.UserGroups(ct);
        var efHubApp = await db.Apps.App(HubInfo.AppKey, ct);
        var efUserGroupsModCategory = await efHubApp.ModCategory(HubInfo.ModCategories.UserGroups, ct);
        var userGroupPermissions = new List<AppUserGroupPermission>();
        foreach (var userGroup in efUserGroups)
        {
            var userGroupPermission = await GetUserGroupPermission(efUserGroupsModCategory, userGroup, ct);
            userGroupPermissions.Add(userGroupPermission);
        }
        return userGroupPermissions.ToArray();
    }

    public async Task<AppUserGroupPermission> GetUserGroupPermission(EfAppUserGroup userGroup, CancellationToken ct)
    {
        var efHubApp = await db.Apps.App(HubInfo.AppKey, ct);
        var efUserGroupsModCategory = await efHubApp.ModCategory(HubInfo.ModCategories.UserGroups, ct);
        var permission = await GetUserGroupPermission(efUserGroupsModCategory, userGroup, ct);
        return permission;
    }

    private async Task<AppUserGroupPermission> GetUserGroupPermission(EfModifierCategory efUserGroupsModCategory, EfAppUserGroup efUserGroup, CancellationToken ct)
    {
        AppUserGroupPermission userGroupPermission;
        var userGroup = efUserGroup.ToModel();
        var efModifier = await efUserGroupsModCategory.AddOrUpdateModifier(userGroup.PublicKey, userGroup.ID, userGroup.GroupName.DisplayText, ct);
        var efUserRoles = await Modifier(efModifier).AssignedRoles(ct);
        var userRoles = efUserRoles.Select(ur => ur.ToModel());
        if (efUserRoles.Any(ur => ur.IsDenyAccess()))
        {
            userGroupPermission = new AppUserGroupPermission(efUserGroup, false, false);
        }
        else
        {
            userGroupPermission = new AppUserGroupPermission
            (
                EfUserGroup: efUserGroup,
                CanView: userRoles
                    .Any(ur => ur.Name.EqualsAny(HubInfo.Roles.UserViewerRoles)),
                CanEdit: userRoles
                    .Any(ur => ur.Name.EqualsAny(HubInfo.Roles.UserEditorRoles))
            );
        }
        return userGroupPermission;
    }

    public async Task<LoggedInAppModel[]> GetLoggedInApps(CancellationToken ct)
    {
        var userIDs = db.Context.Users.Retrieve()
            .Where(u => u.UserName == new AppUserName(user.UserName).Value)
            .Select(u => u.ID);
        var sessionIDs = db.Context.Sessions.Retrieve()
            .Where(s => userIDs.Contains(s.UserID) && s.TimeEnded.Year == 9999)
            .Select(s => s.ID);
        var installationIDs = db.Context.Requests.Retrieve()
            .Where(r => sessionIDs.Contains(r.SessionID))
            .Select(r => r.InstallationID)
            .Distinct();
        var loggedInApps = await db.Context.Installations.Retrieve()
            .Where(inst => installationIDs.Contains(inst.ID))
            .Join
            (
                db.Context.AppVersions.Retrieve(),
                inst => inst.AppVersionID,
                av => av.ID,
                (inst, av) => new { inst.IsCurrent, inst.Domain, av.AppID, av.VersionID }
            )
            .Join
            (
                db.Context.Apps.Retrieve()
                    .Where(a => a.Type == AppType.Values.WebApp),
                joined => joined.AppID,
                a => a.ID,
                (joined, a) => new { joined.IsCurrent, joined.Domain, AppDisplayText = a.DisplayText, joined.VersionID }
            )
            .Join
            (
                db.Context.Versions.Retrieve(),
                joined => joined.VersionID,
                v => v.ID,
                (joined, v) => new { joined.IsCurrent, joined.Domain, joined.AppDisplayText, v.VersionKey }
            )
            .Distinct()
            .ToArrayAsync(ct);
        return loggedInApps
            .Select
            (
                joined => new LoggedInAppModel
                (
                    new AppName(joined.AppDisplayText),
                    joined.IsCurrent ? AppVersionKey.Current : AppVersionKey.Parse(joined.VersionKey),
                    joined.Domain
                )
            )
            .Distinct()
            .ToArray();
    }

    public Task LoggedIn(DateTimeOffset timeLoggedIn, CancellationToken ct) =>
        db.Context.Users.Update
        (
            user,
            u =>
            {
                u.TimeLoggedIn = timeLoggedIn;
            },
            ct
        );

    public AppUserModel ToModel() =>
        new AppUserModel
        (
            ID: ID,
            UserName: new AppUserName(user.UserName),
            Name: new PersonName(user.Name),
            Email: new EmailAddress(user.Email).DisplayText,
            TimeDeactivated: user.TimeDeactivated
        );

    public override string ToString() => $"{nameof(EfAppUser)} {ID}";

}