using XTI_App.Abstractions;
using XTI_Hub.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class EfAppUserGroup
{
    private readonly EfHubDB db;
    private readonly UserGroupEntity userGroup;

    internal EfAppUserGroup(EfHubDB factory, UserGroupEntity entity)
    {
        this.db = factory;
        this.userGroup = entity;
    }

    internal int ID { get => userGroup.ID; }

    public Task<EfAppUser> User(int id, CancellationToken ct) => db.Users.User(this, id, ct);

    public Task<EfAppUser> UserOrAnon(AppUserName userName, CancellationToken ct) => db.Users.UserOrAnon(this, userName, ct);

    public Task<EfAppUser[]> Users(CancellationToken ct) => db.Users.Users(this, ct);

    internal Task<EfAppUser> AddAnonIfNotExists(DateTimeOffset timeAdded, CancellationToken ct) =>
        db.Users.AddAnonIfNotExists(this, timeAdded, ct);

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
    ) => db.Users.AddOrUpdate(this, userName, password, name, email, timeAdded, ct);

    public AppUserGroupModel ToModel() =>
        new AppUserGroupModel
        (
            userGroup.ID,
            new AppUserGroupName(userGroup.DisplayText),
            new ModifierKey(userGroup.DisplayText)
        );

    public override string ToString() => $"{nameof(EfAppUserGroup)} {ToModel()}";
}
