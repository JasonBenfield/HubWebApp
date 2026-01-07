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

    public Task<AppUser[]> Installers(CancellationToken ct) =>
        factory.DB.Users.Retrieve()
            .Where(u => u.UserName.StartsWith("xti_inst"))
            .Select(u => factory.User(u))
            .ToArrayAsync(ct);

    public async Task<AppUser> AddOrUpdateInstaller(string machineName, IHashedPassword hashedPassword, DateTimeOffset now, CancellationToken ct)
    {
        var installer = await InstallerOrAnon(machineName, ct);
        if (installer.ToModel().UserName.Equals(new InstallerUserName(machineName).UserName))
        {
            await installer.ChangePassword(hashedPassword);
        }
        else
        {
            installer = await AddInstaller
            (
                machineName,
                hashedPassword,
                now,
                ct
            );
        }
        var hubApp = await factory.Apps.AppOrUnknown(HubInfo.AppKey, ct);
        if (hubApp.AppKeyEquals(HubInfo.AppKey))
        {
            var role = await hubApp.AddOrUpdateRole(HubInfo.Roles.Admin, ct);
            await installer.AssignRole(role, ct);
        }
        return installer;
    }

    public Task<AppUser> InstallerOrAnon(string machineName, CancellationToken ct)
        => factory.Users.UserOrAnon(new InstallerUserName(machineName).UserName, ct);

    private async Task<AppUser> AddInstaller
    (
        string machineName,
        IHashedPassword password,
        DateTimeOffset timeAdded,
        CancellationToken ct
    )
    {
        var xtiUserGroup = await factory.UserGroups.GetXti(ct);
        var user = await xtiUserGroup.AddOrUpdate
        (
            new InstallerUserName(machineName).UserName,
            password,
            new PersonName($"Installer {machineName}"),
            new EmailAddress(""),
            timeAdded,
            ct
        );
        return user;
    }
}