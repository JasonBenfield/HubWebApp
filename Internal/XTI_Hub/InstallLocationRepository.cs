using Microsoft.EntityFrameworkCore;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class InstallLocationRepository
{
    private readonly AppFactory appFactory;

    public InstallLocationRepository(AppFactory appFactory)
    {
        this.appFactory = appFactory;
    }

    public async Task<InstallLocation> TryAdd(string qualifiedMachineName)
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