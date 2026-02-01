using XTI_App.Abstractions;
using XTI_Hub.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class EfAppUserGroup
{
    private readonly EfHubDB factory;
    private readonly UserGroupEntity entity;

    internal EfAppUserGroup(EfHubDB factory, UserGroupEntity entity)
    {
        this.factory = factory;
        this.entity = entity;
    }

    internal int ID { get => entity.ID; }

    public Task<EfAppUser> User(int id, CancellationToken ct) => factory.Users.User(this, id, ct);

    public Task<EfAppUser> UserOrAnon(AppUserName userName, CancellationToken ct) => factory.Users.UserOrAnon(this, userName, ct);

    public Task<EfAppUser[]> Users(CancellationToken ct) => factory.Users.Users(this, ct);

    internal Task<EfAppUser> AddAnonIfNotExists(DateTimeOffset timeAdded, CancellationToken ct) =>
        factory.Users.AddAnonIfNotExists(this, timeAdded, ct);

    public Task<EfAppUser> AddOrUpdate
    (
        AppUserName userName,
        IHashedPassword password,
        DateTimeOffset timeAdded,
        CancellationToken ct
    ) => AddOrUpdate
        (
            userName,
            password,
            new PersonName(userName.DisplayText),
            new EmailAddress(""),
            timeAdded,
            ct
        );

    public Task<EfAppUser> AddOrUpdate
    (
        AppUserName userName,
        IHashedPassword password,
        PersonName name,
        EmailAddress email,
        DateTimeOffset timeAdded,
        CancellationToken ct
    ) => factory.Users.AddOrUpdate(this, userName, password, name, email, timeAdded, ct);

    public AppUserGroupModel ToModel() =>
        new AppUserGroupModel
        (
            entity.ID,
            new AppUserGroupName(entity.DisplayText),
            new ModifierKey(entity.DisplayText)
        );

    public override string ToString() => $"{nameof(EfAppUserGroup)} {ToModel()}";
}
