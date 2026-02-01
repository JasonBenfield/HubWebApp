using Microsoft.EntityFrameworkCore;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class EfInstallLocations
{
    private readonly EfHubDB db;

    public EfInstallLocations(EfHubDB db)
    {
        this.db = db;
    }

    internal async Task<EfInstallLocation> Location(int id, CancellationToken ct)
    {
        var location = await db.Context.InstallLocations.Retrieve()
            .Where(l => l.ID == id)
            .FirstOrDefaultAsync(ct);
        return new EfInstallLocation(db, location ?? throw new Exception($"Install Location not found with ID {id}"));
    }

    public Task<EfInstallLocation> UnknownLocation(CancellationToken ct) =>
        Location("unknown", ct);

    public async Task<EfInstallation> AddUnknownIfNotFound(EfAppVersion appVersion, CancellationToken ct)
    {
        EfInstallation efInstallation;
        var efLocation = await AddIfNotFound("unknown", ct);
        var hasCurrent = await efLocation.HasCurrentInstallation(appVersion, ct);
        if (!hasCurrent)
        {
            efInstallation = await efLocation.NewCurrentInstallation(appVersion, "", "", DateTimeOffset.Now, ct);
            await efInstallation.BeginInstallation(ct);
            await efInstallation.Installed(ct);
        }
        else
        {
            efInstallation = await efLocation.CurrentInstallation(appVersion, ct);
        }
        return efInstallation;
    }

    public async Task<EfInstallLocation> AddIfNotFound(string qualifiedMachineName, CancellationToken ct)
    {
        qualifiedMachineName = qualifiedMachineName.ToLower().Trim();
        var location = await db.Context.InstallLocations.Retrieve()
            .Where(l => l.QualifiedMachineName == qualifiedMachineName)
            .FirstOrDefaultAsync(ct);
        if (location == null)
        {
            location = new InstallLocationEntity
            {
                QualifiedMachineName = qualifiedMachineName
            };
            await db.Context.InstallLocations.Create(location, ct);
        }
        return new EfInstallLocation(db, location);
    }

    public async Task<EfInstallLocation> Location(string qualifiedMachineName, CancellationToken ct)
    {
        var location = await db.Context.InstallLocations.Retrieve()
            .Where(l => l.QualifiedMachineName == qualifiedMachineName)
            .FirstOrDefaultAsync(ct);
        if (location == null)
        {
            throw new Exception($"Install location '{qualifiedMachineName}' was not found");
        }
        return new EfInstallLocation(db, location);
    }
}