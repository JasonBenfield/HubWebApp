using XTI_App.Abstractions;
using XTI_Hub.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class AppUserGroup
{
    private readonly HubFactory factory;
    private readonly UserGroupEntity entity;

    internal AppUserGroup(HubFactory factory, UserGroupEntity entity)
    {
        this.factory = factory;
        this.entity = entity;
    }

    internal int ID { get => entity.ID; }

    public Task<AppUser> User(int id) => factory.Users.User(this, id);

    public Task<AppUser> UserOrAnon(AppUserName userName) => factory.Users.UserOrAnon(this, userName);

    public Task<AppUser[]> Users() => factory.Users.Users(this);

    internal Task<AppUser> AddAnonIfNotExists(DateTimeOffset timeAdded) =>
        factory.Users.AddAnonIfNotExists(this, timeAdded);

    public Task<AppUser> AddOrUpdate
    (
        AppUserName userName,
        IHashedPassword password,
        DateTimeOffset timeAdded
    ) => AddOrUpdate
        (
            userName,
            password,
            new PersonName(userName.DisplayText),
            new EmailAddress(""),
            timeAdded
        );

    public Task<AppUser> AddOrUpdate
    (
        AppUserName userName,
        IHashedPassword password,
        PersonName name,
        EmailAddress email,
        DateTimeOffset timeAdded
    ) => factory.Users.AddOrUpdate(this, userName, password, name, email, timeAdded);

    public AppUserGroupModel ToModel() =>
        new AppUserGroupModel
        (
            entity.ID,
            new AppUserGroupName(entity.DisplayText),
            new ModifierKey(entity.DisplayText)
        );

    public override string ToString() => $"{nameof(AppUserGroup)} {ToModel()}";
}
