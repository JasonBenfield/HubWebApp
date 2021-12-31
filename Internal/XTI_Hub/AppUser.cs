using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class AppUser : IAppUser
{
    private readonly AppFactory factory;
    private readonly AppUserEntity record;

    internal AppUser(AppFactory factory, AppUserEntity record)
    {
        this.factory = factory;
        this.record = record ?? new AppUserEntity();
        ID = new EntityID(this.record.ID);
    }

    public EntityID ID { get; }
    public AppUserName UserName() => new AppUserName(record.UserName);

    public bool IsPasswordCorrect(IHashedPassword hashedPassword) =>
        hashedPassword.Equals(record.Password);

    public async Task AddRole(AppRole role)
    {
        var app = await role.App();
        var modifier = await app.DefaultModifier();
        await AddRole(role, modifier);
    }

    public Task AddRole(AppRole role, Modifier modifier)
    {
        var record = new AppUserRoleEntity
        {
            UserID = ID.Value,
            RoleID = role.ID.Value,
            ModifierID = modifier.ID.Value
        };
        return factory.DB.UserRoles.Create(record);
    }

    public async Task RemoveRole(Modifier modifier, AppRole role)
    {
        var userRole = await factory.DB
            .UserRoles
            .Retrieve()
            .Where(ur => ur.UserID == ID.Value && ur.ModifierID == modifier.ID.Value && ur.RoleID == role.ID.Value)
            .FirstOrDefaultAsync();
        if (userRole != null)
        {
            await factory.DB.UserRoles.Delete(userRole);
        }
    }

    async Task<IAppRole[]> IAppUser.Roles(IModifier modifier)
        => await AssignedRoles(modifier);

    public Task<AppRole[]> ExplicitlyUnassignedRoles(Modifier modifier)
        => factory.Roles.RolesNotAssignedToUser(this, modifier);

    public async Task<AppRole[]> AssignedRoles(IModifier modifier)
    {
        var roles = await ExplicitlyAssignedRoles(modifier);
        if (!roles.Any() && !modifier.ModKey().Equals(ModifierKey.Default))
        {
            var mod = modifier as Modifier;
            if(mod == null)
            {
                mod = await factory.Modifiers.Modifier(modifier.ID.Value);
            }
            var defaultModifier = await mod.DefaultModifier();
            roles = await ExplicitlyAssignedRoles(defaultModifier);
        }
        return roles;
    }

    public Task<AppRole[]> ExplicitlyAssignedRoles(IModifier modifier)
        => factory.Roles.RolesAssignedToUser(this, modifier);

    public Task ChangePassword(IHashedPassword password)
        => factory.DB.Users.Update(record, u => u.Password = password.Value());

    public Task Edit(PersonName name, EmailAddress email)
        => factory.DB.Users.Update
        (
            record,
            u =>
            {
                u.Name = name.Value;
                u.Email = email.Value;
            }
        );

    public AppUserModel ToModel() => new AppUserModel
    {
        ID = ID.Value,
        UserName = UserName().DisplayText,
        Name = new PersonName(record.Name).DisplayText,
        Email = new EmailAddress(record.Email).DisplayText
    };

    public override string ToString() => $"{nameof(AppUser)} {ID.Value}";
}