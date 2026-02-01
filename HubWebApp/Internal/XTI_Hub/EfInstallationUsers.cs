using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;

namespace XTI_Hub;

public sealed class EfInstallationUsers
{
    private readonly EfHubDB db;

    internal EfInstallationUsers(EfHubDB db)
    {
        this.db = db;
    }

    public Task<EfAppUser[]> Installers(CancellationToken ct) =>
        db.Context.Users.Retrieve()
            .Where(u => u.UserName.StartsWith("xti_inst"))
            .Select(u => new EfAppUser(db, u))
            .ToArrayAsync(ct);

    public async Task<EfAppUser> AddOrUpdateInstaller(string machineName, IHashedPassword hashedPassword, DateTimeOffset now, CancellationToken ct)
    {
        var efInstallationUser = await InstallerOrAnon(machineName, ct);
        if (efInstallationUser.ToModel().UserName.Equals(new InstallerUserName(machineName).UserName))
        {
            await efInstallationUser.ChangePassword(hashedPassword, ct);
        }
        else
        {
            efInstallationUser = await AddInstaller
            (
                machineName,
                hashedPassword,
                now,
                ct
            );
        }
        var efHubApp = await db.Apps.AppOrUnknown(HubInfo.AppKey, ct);
        if (efHubApp.AppKeyEquals(HubInfo.AppKey))
        {
            var efAdminRole = await efHubApp.AddOrUpdateRole(HubInfo.Roles.Admin, ct);
            await efInstallationUser.AssignRole(efAdminRole, ct);
        }
        return efInstallationUser;
    }

    public Task<EfAppUser> InstallerOrAnon(string machineName, CancellationToken ct) => 
        db.Users.UserOrAnon(new InstallerUserName(machineName).UserName, ct);

    private async Task<EfAppUser> AddInstaller
    (
        string machineName,
        IHashedPassword password,
        DateTimeOffset timeAdded,
        CancellationToken ct
    )
    {
        var efUserGroup = await db.UserGroups.GetXti(ct);
        var efUser = await efUserGroup.AddOrUpdate
        (
            new InstallerUserName(machineName).UserName,
            password,
            new PersonName($"Installer {machineName}"),
            new EmailAddress(""),
            timeAdded,
            ct
        );
        return efUser;
    }
}