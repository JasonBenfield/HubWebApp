using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;

namespace XTI_Hub;

public sealed class SystemUserRepository
{
    private readonly AppFactory factory;

    public SystemUserRepository(AppFactory factory)
    {
        this.factory = factory;
    }

    public Task<AppUser[]> SystemUsers()
        => factory.DB
        .Users
        .Retrieve()
        .Where(u => u.UserName.StartsWith("xti_sys_"))
        .Select(u => factory.User(u))
        .ToArrayAsync();

    public Task<AppUser[]> SystemUsers(AppKey appKey)
        => factory.DB
        .Users
        .Retrieve()
        .Where(u => u.UserName.StartsWith(AppUserName.SystemUser(appKey, "").Value))
        .Select(u => factory.User(u))
        .ToArrayAsync();

    public async Task<AppUser> AddOrUpdateSystemUser(AppKey appKey, string machineName, IHashedPassword hashedPassword, DateTimeOffset now)
    {
        var systemUser = await SystemUser(appKey, machineName);
        if (systemUser.UserName().Equals(AppUserName.SystemUser(appKey, machineName)))
        {
            await systemUser.ChangePassword(hashedPassword);
        }
        else
        {
            systemUser = await AddSystemUser
            (
                appKey,
                machineName,
                hashedPassword,
                now
            );
        }
        var app = await factory.Apps.AddOrUpdate(appKey, now);
        var systemRole = await app.AddRoleIfNotFound(AppRoleName.System);
        var modifier = await app.DefaultModifier();
        var assignedRoles = await systemUser.AssignedRoles(modifier);
        if (!assignedRoles.Any(r => r.ID.Equals(systemRole.ID)))
        {
            await systemUser.AddRole(systemRole);
        }
        var hubApp = await factory.Apps.App(HubInfo.AppKey);
        var hubDefaultModifier = await hubApp.DefaultModifier();
        var hubAssignedRoles = await systemUser.AssignedRoles(hubDefaultModifier);
        var publisherRole = await hubApp.AddRoleIfNotFound(HubInfo.Roles.Publisher);
        if (!hubAssignedRoles.Any(r => r.ID.Equals(publisherRole.ID)))
        {
            await systemUser.AddRole(publisherRole);
        }
        var installerRole = await hubApp.AddRoleIfNotFound(HubInfo.Roles.Installer);
        if (!hubAssignedRoles.Any(r => r.ID.Equals(installerRole.ID)))
        {
            await systemUser.AddRole(installerRole);
        }
        return systemUser;
    }

    public Task<AppUser> SystemUser(AppKey appKey, string machineName)
        => factory.Users.UserByUserName(AppUserName.SystemUser(appKey, machineName));

    private async Task<AppUser> AddSystemUser
    (
        AppKey appKey,
        string machineName,
        IHashedPassword password,
        DateTimeOffset timeAdded
    )
    {
        var user = await factory.Users.Add
        (
            AppUserName.SystemUser(appKey, machineName),
            password,
            new PersonName($"{appKey.Name.DisplayText.Replace(" ", "")} {appKey.Type.DisplayText.Replace(" ", "")} {machineName}"),
            new EmailAddress(""),
            timeAdded
        );
        var app = await factory.Apps.App(appKey);
        var role = await app.Role(AppRoleName.System);
        await user.AddRole(role);
        return user;
    }
}