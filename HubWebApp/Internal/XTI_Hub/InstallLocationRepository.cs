using Microsoft.EntityFrameworkCore;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class InstallLocationRepository
{
    private readonly HubFactory hubFactory;

    public InstallLocationRepository(HubFactory hubFactory)
    {
        this.hubFactory = hubFactory;
    }

    internal async Task<InstallLocation> Location(int id, CancellationToken ct)
    {
        var entity = await hubFactory.DB.InstallLocations.Retrieve()
            .Where(l => l.ID == id)
            .FirstOrDefaultAsync(ct);
        return hubFactory.CreateInstallLocation(entity ?? throw new Exception($"Install Location not found with ID {id}"));
    }

    public Task<InstallLocation> UnknownLocation(CancellationToken ct) =>
        Location("unknown", ct);

    public async Task<Installation> AddUnknownIfNotFound(AppVersion appVersion, CancellationToken ct)
    {
        Installation installation;
        var loc = await AddIfNotFound("unknown", ct);
        var hasCurrent = await loc.HasCurrentInstallation(appVersion, ct);
        if (!hasCurrent)
        {
            installation = await loc.NewCurrentInstallation(appVersion, "", "", DateTimeOffset.Now, ct);
            await installation.BeginInstallation(ct);
            await installation.Installed(ct);
        }
        else
        {
            installation = await loc.CurrentInstallation(appVersion, ct);
        }
        return installation;
    }

    public async Task<InstallLocation> AddIfNotFound(string qualifiedMachineName, CancellationToken ct)
    {
        qualifiedMachineName = qualifiedMachineName.ToLower().Trim();
        var location = await hubFactory.DB
            .InstallLocations
            .Retrieve()
            .FirstOrDefaultAsync(l => l.QualifiedMachineName == qualifiedMachineName, ct);
        if (location == null)
        {
            location = new InstallLocationEntity
            {
                QualifiedMachineName = qualifiedMachineName
            };
            await hubFactory.DB
                .InstallLocations
                .Create(location, ct);
        }
        return hubFactory.CreateInstallLocation(location);
    }

    public async Task<InstallLocation> Location(string qualifiedMachineName, CancellationToken ct)
    {
        var location = await hubFactory.DB
            .InstallLocations
            .Retrieve()
            .FirstOrDefaultAsync(l => l.QualifiedMachineName == qualifiedMachineName, ct);
        if (location == null)
        {
            throw new Exception($"Install location '{qualifiedMachineName}' was not found");
        }
        return hubFactory.CreateInstallLocation(location);
    }
}