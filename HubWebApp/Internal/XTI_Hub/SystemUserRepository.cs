using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;

namespace XTI_Hub;

public sealed class SystemUserRepository
{
    private readonly HubFactory factory;

    internal SystemUserRepository(HubFactory factory)
    {
        this.factory = factory;
    }

    public Task<AppUser[]> SystemUsers(AppKey appKey, CancellationToken ct) =>
        factory.DB.Users.Retrieve()
            .Where(u => u.UserName.StartsWith($"xti_sys[{appKey.Serialize()}]") || u.UserName.StartsWith($"xti_sys2[{appKey.Serialize()}]"))
            .Select(u => factory.User(u))
            .ToArrayAsync(ct);

    public async Task<AppUser> AddOrUpdateSystemUser(SystemUserName systemUserName, IHashedPassword hashedPassword, DateTimeOffset now, CancellationToken ct)
    {
        var systemUser = await SystemUserOrAnon(systemUserName, ct);
        if (systemUser.ToModel().UserName.Equals(systemUserName.UserName))
        {
            await systemUser.ChangePassword(hashedPassword);
        }
        else
        {
            systemUser = await AddSystemUser
            (
                systemUserName,
                hashedPassword,
                now,
                ct
            );
        }
        var app = await factory.Apps.App(systemUserName.AppKey, ct);
        var appModel = app.ToModel();
        var selfAdminRole = await app.AddOrUpdateRole(AppRoleName.Admin, ct);
        await systemUser.AssignRole(selfAdminRole, ct);
        var hubApp = await factory.Apps.AppOrUnknown(HubInfo.AppKey, ct);
        if (hubApp.AppKeyEquals(HubInfo.AppKey))
        {
            var hubSystemRole = await hubApp.AddOrUpdateRole(AppRoleName.System, ct);
            await systemUser.AssignRole(hubSystemRole, ct);
            var viewUserRole = await hubApp.AddOrUpdateRole(HubInfo.Roles.ViewUser, ct);
            await systemUser.AssignRole(viewUserRole, ct);
            var appModCategory = await hubApp.AddOrUpdateModCategory(HubInfo.ModCategories.Apps, ct);
            var appModifier = await appModCategory.AddOrUpdateModifier(appModel.PublicKey, appModel.ID, appModel.AppKey.Format(), ct);
            var hubAdmin = await hubApp.AddOrUpdateRole(AppRoleName.Admin, ct);
            await systemUser.Modifier(appModifier).AssignRole(hubAdmin, ct);
            var addStoredObject = await hubApp.AddOrUpdateRole(HubInfo.Roles.AddStoredObject, ct);
            await systemUser.AssignRole(addStoredObject, ct);
        }
        return systemUser;
    }

    public Task<AppUser> SystemUserOrAnon(SystemUserName systemUserName, CancellationToken ct) =>
        factory.Users.UserOrAnon(systemUserName.UserName, ct);

    private async Task<AppUser> AddSystemUser
    (
        SystemUserName systemUserName,
        IHashedPassword password,
        DateTimeOffset timeAdded,
        CancellationToken ct
    )
    {
        var xtiUserGroup = await factory.UserGroups.GetXti(ct);
        var user = await xtiUserGroup.AddOrUpdate
        (
            systemUserName.UserName,
            password,
            new PersonName(systemUserName.UserName.DisplayText),
            new EmailAddress(""),
            timeAdded,
            ct
        );
        return user;
    }
}