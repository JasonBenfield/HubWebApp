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

    public async Task<AppUser> AddOrUpdateInstaller(string machineName, IHashedPassword hashedPassword, DateTimeOffset now)
    {
        var installer = await InstallerOrAnon(machineName);
        if (installer.ToModel().UserName.Equals(new InstallerUserName(machineName).Value))
        {
            await installer.ChangePassword(hashedPassword);
        }
        else
        {
            installer = await AddInstaller
            (
                machineName,
                hashedPassword,
                now
            );
        }
        var hubApp = await factory.Apps.AppOrUnknown(HubInfo.AppKey);
        if (hubApp.AppKeyEquals(HubInfo.AppKey))
        {
            var role = await hubApp.AddRoleIfNotFound(HubInfo.Roles.Admin);
            await installer.AssignRole(role);
        }
        return installer;
    }

    public Task<AppUser> InstallerOrAnon(string machineName)
        => factory.Users.UserOrAnon(new InstallerUserName(machineName).Value);

    private async Task<AppUser> AddInstaller
    (
        string machineName,
        IHashedPassword password,
        DateTimeOffset timeAdded
    )
    {
        var xtiUserGroup = await factory.UserGroups.GetXti();
        var user = await xtiUserGroup.AddOrUpdate
        (
            new InstallerUserName(machineName).Value,
            password,
            new PersonName($"Installer {machineName}"),
            new EmailAddress(""),
            timeAdded
        );
        return user;
    }
}