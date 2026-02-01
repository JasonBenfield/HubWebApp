using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;
using XTI_Hub.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class EfAppUsers
{
    private readonly EfHubDB db;

    public EfAppUsers(EfHubDB db)
    {
        this.db = db;
    }

    public Task<EfAppUser[]> UsersLoggedInBefore(DateTimeOffset maxTime, CancellationToken ct) =>
        db.Context.Users.Retrieve()
            .Where(u => u.TimeLoggedIn < maxTime || (u.TimeLoggedIn.Year == 9999 && u.TimeAdded < maxTime))
            .Select(u => new EfAppUser(db, u))
            .ToArrayAsync(ct);

    internal Task<EfAppUser[]> Users(EfAppUserGroup userGroup, CancellationToken ct) =>
        db.Context.Users.Retrieve()
            .Where(u => u.GroupID == userGroup.ID && u.TimeDeactivated.Year == 9999)
            .OrderBy(u => u.UserName)
            .Select(u => new EfAppUser(db, u))
            .ToArrayAsync(ct);

    public async Task<EfAppUser[]> UsersWithAnyRole(EfModifier modifier, EfAppRole[] roles, CancellationToken ct)
    {
        EfAppUser[] users;
        var roleIDs = roles.Select(r => r.ID).ToArray();
        IQueryable<int> userIDs;
        if (modifier.IsDefault())
        {
            userIDs = db.Context.UserRoles.Retrieve()
                .Where(ur => modifier.ID == ur.ModifierID && roleIDs.Contains(ur.RoleID))
                .Select(ur => ur.UserID);
        }
        else
        {
            var modifierUserIDs = db.Context.UserRoles.Retrieve()
                .Where(ur => modifier.ID == ur.ModifierID && roleIDs.Contains(ur.RoleID))
                .Select(ur => ur.UserID);
            var anyModifiedUserIDs = db.Context.UserRoles.Retrieve()
                .Where(ur => modifier.ID == ur.ModifierID)
                .Select(ur => ur.UserID);
            var efDefaultModifier = await modifier.DefaultModifier(ct);
            userIDs = db.Context.UserRoles.Retrieve()
                .Where
                (
                    ur => modifierUserIDs.Contains(ur.UserID) ||
                    (
                        efDefaultModifier.ID == ur.ModifierID &&
                        roleIDs.Contains(ur.RoleID) &&
                        !anyModifiedUserIDs.Contains(ur.UserID)
                    )
                )
                .Select(ur => ur.UserID);
        }
        users = await db.Context.Users.Retrieve()
            .Where(u => userIDs.Contains(u.ID) && u.TimeDeactivated.Year == 9999)
            .OrderBy(u => u.UserName)
            .Select(u => new EfAppUser(db, u))
            .ToArrayAsync(ct);
        return users;
    }

    internal async Task<EfAppUser> User(EfAppUserGroup userGroup, int id, CancellationToken ct)
    {
        var user = await db.Context.Users.Retrieve()
            .Where(u => u.GroupID == userGroup.ID && u.ID == id)
            .FirstOrDefaultAsync(ct);
        return new EfAppUser(db, user ?? throw new Exception($"User {id} not found"));
    }

    internal async Task<EfAppUser> UserOrAnon(EfAppUserGroup userGroup, AppUserName userName, CancellationToken ct)
    {
        var user = await db.Context.Users.Retrieve()
            .Where(u => u.GroupID == userGroup.ID && u.UserName == userName.Value)
            .FirstOrDefaultAsync(ct);
        if (user == null)
        {
            user = await GetUser(AppUserName.Anon, ct);
        }
        return new EfAppUser(db, user ?? throw new Exception("User not found"));
    }

    public async Task<EfAppUser> User(int id, CancellationToken ct)
    {
        var user = await db.Context.Users.Retrieve()
            .Where(u => u.ID == id)
            .FirstOrDefaultAsync(ct);
        return new EfAppUser(db, user ?? throw new Exception($"User {id} not found"));
    }

    public async Task<EfAppUser> UserOrAnon(AppUserName userName, CancellationToken ct)
    {
        if (userName.IsBlank())
        {
            userName = AppUserName.Anon;
        }
        var user = await GetUser(userName, ct);
        if (user == null && !userName.IsAnon())
        {
            user = await GetUser(AppUserName.Anon, ct);
        }
        return new EfAppUser(db, user ?? throw new ArgumentNullException(nameof(user)));
    }

    public Task<EfAppUser> Anon(CancellationToken ct) => UserByUserName(AppUserName.Anon, ct);

    public async Task<bool> UserNameExists(AppUserName userName, CancellationToken ct)
    {
        var user = await GetUser(userName, ct);
        return user != null;
    }

    public async Task<EfAppUser> UserByUserName(AppUserName userName, CancellationToken ct)
    {
        var user = await GetUser(userName, ct);
        return new EfAppUser
        (
            db,
            user
            ?? throw new ArgumentNullException(nameof(user), $"User not found with user name '{userName.Value}'")
        );
    }

    public async Task<EfAppUser> UserOrAnonByExternalKey(AuthenticatorKey authenticatorKey, string externalUserKey, CancellationToken ct)
    {
        var authenticatorIDs = db.Context
            .Authenticators.Retrieve()
            .Where(a => a.AuthenticatorKey == authenticatorKey.Value)
            .Select(a => a.ID);
        var userIDs = db.Context
            .UserAuthenticators.Retrieve()
            .Where
            (
                a =>
                    authenticatorIDs.Contains(a.AuthenticatorID)
                    && a.ExternalUserKey == externalUserKey
            )
            .Select(a => a.UserID);
        var user = await db.Context.Users.Retrieve()
            .Where(u => userIDs.Contains(u.ID))
            .FirstOrDefaultAsync(ct);
        if (user == null)
        {
            user = await GetUser(AppUserName.Anon, ct);
        }
        return new EfAppUser(db, user ?? throw new ExternalUserNotFoundException(authenticatorKey, externalUserKey));
    }

    private Task<AppUserEntity?> GetUser(AppUserName userName, CancellationToken ct) =>
        db.Context.Users.Retrieve()
            .Where(u => u.UserName == userName.Value)
            .FirstOrDefaultAsync(ct);

    internal async Task<EfAppUser> AddAnonIfNotExists(EfAppUserGroup userGroup, DateTimeOffset timeAdded, CancellationToken ct)
    {
        var userName = AppUserName.Anon;
        var user = await GetUser(userName, ct);
        if (user == null)
        {
            user = await AddUserEntity
            (
                userGroup.ID,
                userName,
                new SystemHashedPassword(),
                new PersonName(""),
                new EmailAddress(""),
                timeAdded,
                ct
            );
        }
        return new EfAppUser(db, user);
    }

    private class SystemHashedPassword : IHashedPassword
    {
        public bool Equals(string? other) => false;

        public string Value() => new GeneratedKey().Value();
    }

    internal async Task<EfAppUser> AddOrUpdate
    (
        EfAppUserGroup userGroup,
        AppUserName userName,
        IHashedPassword password,
        PersonName name,
        EmailAddress email,
        DateTimeOffset timeAdded,
        CancellationToken ct
    )
    {
        var user = await GetUser(userName, ct);
        if (user == null)
        {
            user = await AddUserEntity(userGroup.ID, userName, password, name, email, timeAdded, ct);
        }
        else
        {
            await db.Context.Users.Update
            (
                user,
                u =>
                {
                    u.Name = name.Value;
                    u.Password = password.Value();
                    u.Email = email.Value;
                    u.TimeDeactivated = DateTimeOffset.MaxValue;
                },
                ct
            );
        }
        return new EfAppUser(db, user);
    }

    private async Task<AppUserEntity> AddUserEntity(int userGroupID, AppUserName userName, IHashedPassword password, PersonName name, EmailAddress email, DateTimeOffset timeAdded, CancellationToken ct)
    {
        var newUser = new AppUserEntity
        {
            GroupID = userGroupID,
            UserName = userName.Value,
            Password = password.Value(),
            Name = name.Value,
            Email = email.Value,
            TimeAdded = timeAdded,
            TimeDeactivated = DateTimeOffset.MaxValue
        };
        await db.Context.Users.Create(newUser, ct);
        return newUser;
    }
}