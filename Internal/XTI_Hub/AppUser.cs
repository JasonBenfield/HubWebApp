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
        await Modifier(modifier).AddRole(role);
    }

    public AppUserModifier Modifier(Modifier modifier) =>
        new AppUserModifier(factory, this, modifier);

    async Task<IAppRole[]> IAppUser.Roles(IModifier modifier)
    {
        var mod = modifier as Modifier;
        if (mod == null)
        {
            mod = await factory.Modifiers.Modifier(modifier.ID.Value);
        }
        var roles = await Modifier(mod).AssignedRoles();
        return roles;
    }

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