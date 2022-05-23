using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;

namespace XTI_Hub;

public sealed class InstallationUserRepository
{
    private readonly HubFactory factory;

    internal InstallationUserRepository(HubFactory factory)
    {
        this.factory = factory;
    }

    public Task<AppUser[]> InstallationUsers() =>
        factory.DB
            .Users
            .Retrieve()
            .Where(u => u.UserName.StartsWith("xti_inst_"))
            .Select(u => factory.User(u))
            .ToArrayAsync();

    public async Task<AppUser> AddOrUpdateInstallationUser(string machineName, IHashedPassword hashedPassword, DateTimeOffset now)
    {
        var installationUser = await InstallationUserOrAnon(machineName);
        if (installationUser.UserName().Equals(AppUserName.InstallationUser(machineName)))
        {
            await installationUser.ChangePassword(hashedPassword);
        }
        else
        {
            installationUser = await AddInstallationUser
            (
                machineName,
                hashedPassword,
                now
            );
        }
        return installationUser;
    }

    public Task<AppUser> InstallationUserOrAnon(string machineName)
        => factory.Users.UserOrAnon(AppUserName.InstallationUser(machineName));

    private async Task<AppUser> AddInstallationUser
    (
        string machineName,
        IHashedPassword password,
        DateTimeOffset timeAdded
    )
    {
        var user = await factory.Users.AddOrUpdate
        (
            AppUserName.InstallationUser(machineName),
            password,
            new PersonName($"Installer {machineName}"),
            new EmailAddress(""),
            timeAdded
        );
        var hubApp = await factory.Apps.App(HubInfo.AppKey);
        var role = await hubApp.AddRoleIfNotFound(HubInfo.Roles.Admin);
        await user.AssignRole(role);
        return user;
    }
}