using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;
using XTI_Hub.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class AppUser : IAppUser
{
    private readonly HubFactory factory;
    private readonly AppUserEntity record;

    internal AppUser(HubFactory factory, AppUserEntity record)
    {
        this.factory = factory;
        this.record = record ?? new AppUserEntity();
        ID = this.record.ID;
    }

    public int ID { get; }
    public AppUserName UserName() => new AppUserName(record.UserName);

    public bool IsPasswordCorrect(IHashedPassword hashedPassword) =>
        hashedPassword.Equals(record.Password);

    public async Task AssignRole(AppRole role)
    {
        var app = await role.App();
        var modifier = await app.DefaultModifier();
        await Modifier(modifier).AssignRole(role);
    }

    public AppUserModifier Modifier(Modifier modifier) =>
        new AppUserModifier(factory, this, modifier);

    async Task<IAppRole[]> IAppUser.Roles(IModifier modifier)
    {
        var mod = modifier as Modifier;
        if (mod == null)
        {
            mod = await factory.Modifiers.Modifier(modifier.ID);
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

    public async Task AddAuthenticator(App authenticatorApp, string externalUserKey)
    {
        var authenticatorIDs = factory.DB
            .Authenticators.Retrieve()
            .Where(a => a.AppID == authenticatorApp.ID)
            .Select(a => a.ID);
        var entity = await factory.DB
            .UserAuthenticators.Retrieve()
            .Where
            (
                ua =>
                    authenticatorIDs.Contains(ua.AuthenticatorID)
                    && ua.UserID == ID
            )
            .FirstOrDefaultAsync();
        if (entity == null)
        {
            var authenticatorID = await authenticatorIDs.FirstAsync();
            entity = new UserAuthenticatorEntity
            {
                AuthenticatorID = authenticatorID,
                UserID = ID,
                ExternalUserKey = externalUserKey
            };
            await factory.DB.UserAuthenticators.Create(entity);
        }
        else
        {
            await factory.DB.UserAuthenticators.Update
            (
                entity,
                ua => ua.ExternalUserKey = externalUserKey
            );
        }
    }

    public AppUserModel ToModel() => new AppUserModel
    {
        ID = ID,
        UserName = UserName().DisplayText,
        Name = new PersonName(record.Name).DisplayText,
        Email = new EmailAddress(record.Email).DisplayText
    };

    public override string ToString() => $"{nameof(AppUser)} {ID}";
}