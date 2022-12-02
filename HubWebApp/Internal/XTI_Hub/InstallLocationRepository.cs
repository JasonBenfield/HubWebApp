using Microsoft.EntityFrameworkCore;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class InstallLocationRepository
{
    private readonly HubFactory appFactory;

    public InstallLocationRepository(HubFactory appFactory)
    {
        this.appFactory = appFactory;
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
            installation = await loc.NewCurrentInstallation(appVersion, "", DateTimeOffset.Now);
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
        var location = await appFactory.DB
            .InstallLocations
            .Retrieve()
            .FirstOrDefaultAsync(l => l.QualifiedMachineName == qualifiedMachineName);
        if (location == null)
        {
            location = new InstallLocationEntity
            {
                QualifiedMachineName = qualifiedMachineName
            };
            await appFactory.DB
                .InstallLocations
                .Create(location);
        }
        return appFactory.CreateInstallLocation(location);
    }

    public async Task<InstallLocation> Location(string qualifiedMachineName)
    {
        var location = await appFactory.DB
            .InstallLocations
            .Retrieve()
            .FirstOrDefaultAsync(l => l.QualifiedMachineName == qualifiedMachineName);
        if (location == null)
        {
            throw new Exception($"Install location '{qualifiedMachineName}' was not found");
        }
        return appFactory.CreateInstallLocation(location);
    }
}