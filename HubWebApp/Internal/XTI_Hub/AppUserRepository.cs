using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;
using XTI_Hub.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class AppUserRepository
{
    private readonly HubFactory factory;

    public AppUserRepository(HubFactory factory)
    {
        this.factory = factory;
    }

    internal Task<AppUser[]> Users(AppUserGroup userGroup)
        => factory.DB
            .Users
            .Retrieve()
            .Where(u => u.GroupID == userGroup.ID)
            .OrderBy(u => u.UserName)
            .Select(u => factory.User(u))
            .ToArrayAsync();

    public async Task<AppUser[]> UsersWithAnyRole(Modifier modifier, AppRole[] roles)
    {
        AppUser[] users;
        var roleIDs = roles.Select(r => r.ID).ToArray();
        IQueryable<int> userIDs;
        if (modifier.IsDefault())
        {
            userIDs = factory.DB
                .UserRoles.Retrieve()
                .Where(ur => modifier.ID == ur.ModifierID && roleIDs.Contains(ur.RoleID))
                .Select(ur => ur.UserID);
        }
        else
        {
            var modifierUserIDs = factory.DB
                .UserRoles.Retrieve()
                .Where(ur => modifier.ID == ur.ModifierID && roleIDs.Contains(ur.RoleID))
                .Select(ur => ur.UserID);
            var anyModifiedUserIDs = factory.DB
                .UserRoles.Retrieve()
                .Where(ur => modifier.ID == ur.ModifierID)
                .Select(ur => ur.UserID);
            var defaultModifier = await modifier.DefaultModifier();
            userIDs = factory.DB
                .UserRoles.Retrieve()
                .Where
                (
                    ur => modifierUserIDs.Contains(ur.UserID) ||
                    (
                        defaultModifier.ID == ur.ModifierID &&
                        roleIDs.Contains(ur.RoleID) &&
                        !anyModifiedUserIDs.Contains(ur.UserID)
                    )
                )
                .Select(ur => ur.UserID);
        }
        users = await factory.DB
            .Users
            .Retrieve()
            .Where(u => userIDs.Contains(u.ID))
            .OrderBy(u => u.UserName)
            .Select(u => factory.User(u))
            .ToArrayAsync();
        return users;
    }

    internal async Task<AppUser> User(AppUserGroup userGroup, int id)
    {
        var userRecord = await factory.DB
            .Users
            .Retrieve()
            .FirstOrDefaultAsync(u => u.GroupID == userGroup.ID && u.ID == id);
        return factory.User(userRecord ?? throw new Exception($"User {id} not found"));
    }

    internal async Task<AppUser> UserOrAnon(AppUserGroup userGroup, AppUserName userName)
    {
        var userRecord = await factory.DB
            .Users
            .Retrieve()
            .FirstOrDefaultAsync(u => u.GroupID == userGroup.ID && u.UserName == userName.Value);
        if (userRecord == null)
        {
            userRecord = await GetUser(AppUserName.Anon);
        }
        return factory.User(userRecord ?? throw new Exception("User not found"));
    }

    public async Task<AppUser> User(int id)
    {
        var userRecord = await factory.DB
            .Users
            .Retrieve()
            .FirstOrDefaultAsync(u => u.ID == id);
        return factory.User(userRecord ?? throw new Exception($"User {id} not found"));
    }

    public async Task<AppUser> UserOrAnon(AppUserName userName)
    {
        var record = await GetUser(userName);
        if (record == null && !userName.IsAnon())
        {
            record = await GetUser(AppUserName.Anon);
        }
        return factory.User(record ?? throw new ArgumentNullException(nameof(record)));
    }

    public Task<AppUser> Anon() => UserByUserName(AppUserName.Anon);

    public async Task<bool> UserNameExists(AppUserName userName)
    {
        var record = await GetUser(userName);
        return record != null;
    }

    public async Task<AppUser> UserByUserName(AppUserName userName)
    {
        var record = await GetUser(userName);
        return factory.User
        (
            record
            ?? throw new ArgumentNullException(nameof(record), $"User not found with user name '{userName.Value}'")
        );
    }

    public async Task<AppUser> UserByExternalKey(AuthenticatorKey authenticatorKey, string externalUserKey)
    {
        var authenticatorIDs = factory.DB
            .Authenticators.Retrieve()
            .Where(a => a.AuthenticatorKey == authenticatorKey.Value)
            .Select(a => a.ID);
        var userIDs = factory.DB
            .UserAuthenticators.Retrieve()
            .Where
            (
                a =>
                    authenticatorIDs.Contains(a.AuthenticatorID)
                    && a.ExternalUserKey == externalUserKey
            )
            .Select(a => a.UserID);
        var userEntity = await factory.DB
            .Users.Retrieve()
            .Where(u => userIDs.Contains(u.ID))
            .FirstOrDefaultAsync();
        return factory.User
        (
            userEntity
            ?? throw new ExternalUserNotFoundException(authenticatorKey, externalUserKey)
        );
    }

    private Task<AppUserEntity?> GetUser(AppUserName userName)
        => factory.DB
            .Users
            .Retrieve()
            .FirstOrDefaultAsync(u => u.UserName == userName.Value);

    internal async Task<AppUser> AddAnonIfNotExists(AppUserGroup userGroup, DateTimeOffset timeAdded)
    {
        var userName = AppUserName.Anon;
        var record = await GetUser(userName);
        if (record == null)
        {
            record = await AddUserEntity
            (
                userGroup.ID,
                userName,
                new SystemHashedPassword(),
                new PersonName(""),
                new EmailAddress(""),
                timeAdded
            );
        }
        return factory.User(record);
    }

    private class SystemHashedPassword : IHashedPassword
    {
        public bool Equals(string? other) => false;

        public string Value() => new GeneratedKey().Value();
    }

    internal async Task<AppUser> AddOrUpdate
    (
        AppUserGroup userGroup,
        AppUserName userName,
        IHashedPassword password,
        PersonName name,
        EmailAddress email,
        DateTimeOffset timeAdded
    )
    {
        var record = await GetUser(userName);
        if (record == null)
        {
            record = await AddUserEntity(userGroup.ID, userName, password, name, email, timeAdded);
        }
        else
        {
            await factory.DB.Users.Update
            (
                record,
                u =>
                {
                    u.Name = name.Value;
                    u.Password = password.Value();
                    u.Email = email.Value;
                }
            );
        }
        return factory.User(record);
    }

    private async Task<AppUserEntity> AddUserEntity(int userGroupID, AppUserName userName, IHashedPassword password, PersonName name, EmailAddress email, DateTimeOffset timeAdded)
    {
        var newUser = new AppUserEntity
        {
            GroupID = userGroupID,
            UserName = userName.Value,
            Password = password.Value(),
            Name = name.Value,
            Email = email.Value,
            TimeAdded = timeAdded
        };
        await factory.DB.Users.Create(newUser);
        return newUser;
    }
}