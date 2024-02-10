using Microsoft.EntityFrameworkCore;

namespace XTI_Hub;

public sealed class AppUserRoleRepository
{
    private readonly HubFactory factory;

    public AppUserRoleRepository(HubFactory factory)
    {
        this.factory = factory;
    }

    public async Task<AppUserRole> UserRole(int id)
    {
        var userRole = await factory.DB.UserRoles.Retrieve()
            .Where(ur => ur.ID == id)
            .FirstOrDefaultAsync();
        return new AppUserRole(factory, userRole ?? throw new ArgumentException($"User Role {id} was not found."));
    }
}
