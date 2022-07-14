using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;

namespace XTI_Hub;

public sealed class InstallerRepository
{
    private readonly HubFactory factory;

    internal InstallerRepository(HubFactory factory)
    {
        this.factory = factory;
    }

    public Task<AppUser[]> Installers() =>
        factory.DB
            .Users
            .Retrieve()
            .Where(u => u.UserName.StartsWith("xti_inst"))
            .Select(u => factory.User(u))
            .ToArrayAsync();

    public async Task<AppUser> AddOrUpdateInstallationUser(string machineName, IHashedPassword hashedPassword, DateTimeOffset now)
    {
        var installationUser = await InstallationUserOrAnon(machineName);
        if (installationUser.UserName().Equals(new InstallerUserName(machineName).Value))
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
        => factory.Users.UserOrAnon(new InstallerUserName(machineName).Value);

    private async Task<AppUser> AddInstallationUser
    (
        string machineName,
        IHashedPassword password,
        DateTimeOffset timeAdded
    )
    {
        var user = await factory.Users.AddOrUpdate
        (
            new InstallerUserName(machineName).Value,
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