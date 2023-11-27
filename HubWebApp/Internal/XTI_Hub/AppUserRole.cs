using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class AppUserRole
{
    private readonly HubFactory factory;
    private readonly AppUserRoleEntity userRole;

    public AppUserRole(HubFactory factory, AppUserRoleEntity userRole)
    {
        this.factory = factory;
        this.userRole = userRole;
    }

    public int ID { get => userRole.ID; }

    public Task<AppUser> User() => factory.Users.User(userRole.UserID);

    public Task<Modifier> Modifier() => factory.Modifiers.Modifier(userRole.ModifierID);

    public Task<AppRole> Role() => factory.Roles.Role(userRole.RoleID);

}
