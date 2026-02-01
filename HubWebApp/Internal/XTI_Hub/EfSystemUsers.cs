using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;

namespace XTI_Hub;

public sealed class EfSystemUsers
{
    private readonly EfHubDB db;

    internal EfSystemUsers(EfHubDB db)
    {
        this.db = db;
    }

    public Task<EfAppUser[]> SystemUsers(AppKey appKey, CancellationToken ct) =>
        db.Context.Users.Retrieve()
            .Where(u => u.UserName.StartsWith($"xti_sys[{appKey.Serialize()}]") || u.UserName.StartsWith($"xti_sys2[{appKey.Serialize()}]"))
            .Select(u => new EfAppUser(db, u))
            .ToArrayAsync(ct);

    public async Task<EfAppUser> AddOrUpdateSystemUser(SystemUserName systemUserName, IHashedPassword hashedPassword, DateTimeOffset now, CancellationToken ct)
    {
        var efSystemUser = await SystemUserOrAnon(systemUserName, ct);
        if (efSystemUser.ToModel().UserName.Equals(systemUserName.UserName))
        {
            await efSystemUser.ChangePassword(hashedPassword, ct);
        }
        else
        {
            efSystemUser = await AddSystemUser
            (
                systemUserName,
                hashedPassword,
                now,
                ct
            );
        }
        var efApp = await db.Apps.App(systemUserName.AppKey, ct);
        var app = efApp.ToModel();
        var efSelfAdminRole = await efApp.AddOrUpdateRole(AppRoleName.Admin, ct);
        await efSystemUser.AssignRole(efSelfAdminRole, ct);
        var efHubApp = await db.Apps.AppOrUnknown(HubInfo.AppKey, ct);
        if (efHubApp.AppKeyEquals(HubInfo.AppKey))
        {
            var efHubSystemRole = await efHubApp.AddOrUpdateRole(AppRoleName.System, ct);
            await efSystemUser.AssignRole(efHubSystemRole, ct);
            var efViewUserRole = await efHubApp.AddOrUpdateRole(HubInfo.Roles.ViewUser, ct);
            await efSystemUser.AssignRole(efViewUserRole, ct);
            var efAppModCategory = await efHubApp.AddOrUpdateModCategory(HubInfo.ModCategories.Apps, ct);
            var efAppModifier = await efAppModCategory.AddOrUpdateModifier(app.PublicKey, app.ID, app.AppKey.Format(), ct);
            var efHubAdminRole = await efHubApp.AddOrUpdateRole(AppRoleName.Admin, ct);
            await efSystemUser.Modifier(efAppModifier).AssignRole(efHubAdminRole, ct);
            var efAddStoredObjectRole = await efHubApp.AddOrUpdateRole(HubInfo.Roles.AddStoredObject, ct);
            await efSystemUser.AssignRole(efAddStoredObjectRole, ct);
        }
        return efSystemUser;
    }

    public Task<EfAppUser> SystemUserOrAnon(SystemUserName systemUserName, CancellationToken ct) =>
        db.Users.UserOrAnon(systemUserName.UserName, ct);

    private async Task<EfAppUser> AddSystemUser
    (
        SystemUserName systemUserName,
        IHashedPassword password,
        DateTimeOffset timeAdded,
        CancellationToken ct
    )
    {
        var efUserGroup = await db.UserGroups.GetXti(ct);
        var efUser = await efUserGroup.AddOrUpdate
        (
            systemUserName.UserName,
            password,
            new PersonName(systemUserName.UserName.DisplayText),
            new EmailAddress(""),
            timeAdded,
            ct
        );
        return efUser;
    }
}