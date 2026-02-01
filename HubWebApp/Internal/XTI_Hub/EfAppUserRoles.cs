using Microsoft.EntityFrameworkCore;

namespace XTI_Hub;

public sealed class EfAppUserRoles
{
    private readonly EfHubDB factory;

    public EfAppUserRoles(EfHubDB factory)
    {
        this.factory = factory;
    }

    public async Task<EfAppUserRole> UserRole(int id, CancellationToken ct)
    {
        var userRole = await factory.Context.UserRoles.Retrieve()
            .Where(ur => ur.ID == id)
            .FirstOrDefaultAsync(ct);
        return new EfAppUserRole(factory, userRole ?? throw new ArgumentException($"User Role {id} was not found."));
    }
}
