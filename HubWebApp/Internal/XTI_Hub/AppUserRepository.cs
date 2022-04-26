﻿using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class AppUserRepository
{
    private readonly AppFactory factory;

    public AppUserRepository(AppFactory factory)
    {
        this.factory = factory;
    }

    public Task<AppUser[]> Users()
        => factory.DB
            .Users
            .Retrieve()
            .OrderBy(u => u.UserName)
            .Select(u => factory.User(u))
            .ToArrayAsync();

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
        if (record == null && !userName.Equals(AppUserName.Anon))
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
        return factory.User(record ?? throw new ArgumentNullException(nameof(record)));
    }

    public async Task<AppUser> UserByExternalKey(App authenticatorApp, string externalUserKey)
    {
        var authenticatorIDs = factory.DB
            .Authenticators.Retrieve()
            .Where(a => a.AppID == authenticatorApp.ID.Value)
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
            ?? throw new Exception($"User not found for authenticator '{authenticatorApp.Key().Name.DisplayText}' with user key '{externalUserKey}'")
        );
    }

    private Task<AppUserEntity?> GetUser(AppUserName userName)
        => factory.DB
            .Users
            .Retrieve()
            .FirstOrDefaultAsync(u => u.UserName == userName.Value);

    internal async Task<AppUser> AddAnonIfNotExists(DateTimeOffset timeAdded)
    {
        var userName = AppUserName.Anon;
        var record = await GetUser(userName);
        if (record == null)
        {
            record = await AddUserEntity
            (
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

    public Task<AppUser> Add
    (
        AppUserName userName,
        IHashedPassword password,
        DateTimeOffset timeAdded
    ) => AddOrUpdate(userName, password, new PersonName(""), new EmailAddress(""), timeAdded);

    public async Task<AppUser> AddOrUpdate
    (
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
            record = await AddUserEntity(userName, password, name, email, timeAdded);
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

    private async Task<AppUserEntity> AddUserEntity(AppUserName userName, IHashedPassword password, PersonName name, EmailAddress email, DateTimeOffset timeAdded)
    {
        var newUser = new AppUserEntity
        {
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