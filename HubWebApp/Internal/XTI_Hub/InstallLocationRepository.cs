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

    internal async Task<InstallLocation> Location(int id)
    {
        var entity = await hubFactory.DB.InstallLocations.Retrieve()
            .Where(l => l.ID == id)
            .FirstOrDefaultAsync();
        return hubFactory.CreateInstallLocation(entity ?? throw new Exception($"Install Location not found with ID {id}"));
    }

    public Task<InstallLocation> UnknownLocation() =>
        Location("unknown");

    public async Task<Installation> AddUnknownIfNotFound(AppVersion appVersion)
    {
        Installation installation;
        var loc = await AddIfNotFound("unknown");
        var hasCurrent = await loc.HasCurrentInstallation(appVersion);
        if (!hasCurrent)
        {
            installation = await loc.NewCurrentInstallation(appVersion, "", "", DateTimeOffset.Now);
            await installation.BeginInstallation();
            await installation.Installed();
        }
        else
        {
            installation = await loc.CurrentInstallation(appVersion);
        }
        return installation;
    }

    public async Task<InstallLocation> AddIfNotFound(string qualifiedMachineName)
    {
        qualifiedMachineName = qualifiedMachineName.ToLower().Trim();
        var location = await hubFactory.DB
            .InstallLocations
            .Retrieve()
            .FirstOrDefaultAsync(l => l.QualifiedMachineName == qualifiedMachineName);
        if (location == null)
        {
            location = new InstallLocationEntity
            {
                QualifiedMachineName = qualifiedMachineName
            };
            await hubFactory.DB
                .InstallLocations
                .Create(location);
        }
        return hubFactory.CreateInstallLocation(location);
    }

    public async Task<InstallLocation> Location(string qualifiedMachineName)
    {
        var location = await hubFactory.DB
            .InstallLocations
            .Retrieve()
            .FirstOrDefaultAsync(l => l.QualifiedMachineName == qualifiedMachineName);
        if (location == null)
        {
            throw new Exception($"Install location '{qualifiedMachineName}' was not found");
        }
        return hubFactory.CreateInstallLocation(location);
    }
}