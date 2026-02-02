using Microsoft.EntityFrameworkCore;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class AppUserModifier
{
    private readonly EfHubDB db;
    private readonly EfAppUser efUser;

    internal AppUserModifier(EfHubDB db, EfAppUser efUser, EfModifier efModifier)
    {
        this.db = db;
        this.efUser = efUser;
        EfModifier = efModifier;
    }

    public EfModifier EfModifier { get; }

    public async Task AssignRole(EfAppRole role, CancellationToken ct)
    {
        var any = await GetUserRole(role).AnyAsync(ct);
        if (!any)
        {
            var userRole = new AppUserRoleEntity
            {
                UserID = efUser.ID,
                RoleID = role.ID,
                ModifierID = EfModifier.ID
            };
            await db.Context.UserRoles.Create(userRole, ct);
        }
    }

    public async Task UnassignRole(EfAppRole role, CancellationToken ct)
    {
        var userRole = await GetUserRole(role).FirstOrDefaultAsync(ct);
        if (userRole != null)
        {
            await db.Context.UserRoles.Delete(userRole, ct);
        }
    }

    private IQueryable<AppUserRoleEntity> GetUserRole(EfAppRole role) =>
        db.Context.UserRoles.Retrieve()
            .Where
            (
                ur => ur.UserID == efUser.ID
                    && ur.ModifierID == EfModifier.ID
                    && ur.RoleID == role.ID
            );

    public Task<EfAppRole[]> ExplicitlyUnassignedRoles(CancellationToken ct) => 
        db.Roles.RolesNotAssignedToUser(efUser, EfModifier, ct);

    public async Task<EfAppRole[]> AssignedRoles(CancellationToken ct)
    {
        var efRoles = await ExplicitlyAssignedRoles(ct);
        if (!efRoles.Any() && !EfModifier.IsDefault())
        {
            var efDefaultModifier = await EfModifier.DefaultModifier(ct);
            efRoles = await new AppUserModifier(db, efUser, efDefaultModifier).AssignedRoles(ct);
        }
        return efRoles;
    }

    public Task<EfAppRole[]> ExplicitlyAssignedRoles(CancellationToken ct) => 
        db.Roles.RolesAssignedToUser(efUser, EfModifier, ct);


}