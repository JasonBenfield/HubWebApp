﻿using Microsoft.EntityFrameworkCore;
using XTI_Core.Fakes;
using XTI_HubDB.EF;
using XTI_HubWebAppApiActions.Storage;

namespace HubWebApp.Tests;

internal sealed class StoreObjectTest
{
    [Test]
    public async Task ShouldAllowAnonymousUserToGetStoredObject()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
        var request = new StoreObjectRequest
        {
            StorageName = "something",
            Data = "Whatever"
        };
        var storageKey = await tester.Execute(request);
        tester.Logout();
        var getStoredObj = tester.Create((hubApi) => hubApi.Storage.GetStoredObject);
        var modifier = await tester.DefaultModifier();
        AccessAssertions.Create(getStoredObj).ShouldAllowAnonymous
        (
            new GetStoredObjectRequest
            {
                StorageName = request.StorageName,
                StorageKey = storageKey
            }
        );
    }

    [Test]
    public async Task ShouldThrowError_WhenRoleIsNotAssignedToUser()
    {
        var tester = await Setup();
        var request = new StoreObjectRequest
        {
            StorageName = "something",
            Data = "Whatever"
        };
        await AccessAssertions.Create(tester)
            .ShouldThrowError_WhenAccessIsDenied
            (
                request,
                HubInfo.Roles.Admin,
                HubInfo.Roles.AddStoredObject
            );
    }

    [Test]
    public async Task ShouldRequireStorageName()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
        var request = new StoreObjectRequest
        {
            StorageName = "",
            Data = "Whatever"
        };
        var ex = Assert.ThrowsAsync<ValidationFailedException>(() => tester.Execute(request));
        Assert.That(ex?.Errors.Select(err => err.Message), Is.EquivalentTo(new[] { StorageErrors.StorageNameIsRequired }));
    }

    [Test]
    public async Task ShouldRequireData()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
        var request = new StoreObjectRequest
        {
            StorageName = "something",
            Data = ""
        };
        var ex = Assert.ThrowsAsync<ValidationFailedException>(() => tester.Execute(request));
        Assert.That(ex?.Errors.Select(err => err.Message), Is.EquivalentTo(new[] { StorageErrors.StorageDataIsRequired }));
    }

    [Test]
    public async Task ShouldRequireStorageName_WhenGettingStoredObject()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
        var request = new StoreObjectRequest
        {
            StorageName = "something",
            Data = "Whatever"
        };
        var storageKey = await tester.Execute(request);
        var ex = Assert.ThrowsAsync<ValidationFailedException>(() => GetStoredObject(tester, "", storageKey));
        Assert.That(ex?.Errors.Select(err => err.Message), Is.EquivalentTo(new[] { StorageErrors.StorageNameIsRequired }));
    }

    [Test]
    public async Task ShouldRequireStorageKey_WhenGettingStoredObject()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
        var request = new StoreObjectRequest
        {
            StorageName = "something",
            Data = "Whatever"
        };
        var storageKey = await tester.Execute(request);
        var ex = Assert.ThrowsAsync<ValidationFailedException>(() => GetStoredObject(tester, request.StorageName, ""));
        Assert.That(ex?.Errors.Select(err => err.Message), Is.EquivalentTo(new[] { StorageErrors.StorageKeyIsRequired }));
    }

    [Test]
    public async Task ShouldGetStoredObject()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
        var request = new StoreObjectRequest
        {
            StorageName = "something",
            Data = "Whatever"
        };
        var storageKey = await tester.Execute(request);
        var stored = await GetStoredObject(tester, request.StorageName, storageKey);
        Assert.That(stored, Is.EqualTo(request.Data));
    }

    [Test]
    public async Task StorageNameShouldNotBeCaseSensitive()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
        var request = new StoreObjectRequest
        {
            StorageName = "Something",
            Data = "Whatever"
        };
        var storageKey = await tester.Execute(request);
        var stored = await GetStoredObject(tester, "SomEthinG", storageKey);
        Assert.That(stored, Is.EqualTo(request.Data));
    }

    [Test]
    public async Task StorageNameShouldIgnoreSpaces()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
        var request = new StoreObjectRequest
        {
            StorageName = "Something 1",
            Data = "Whatever"
        };
        var storageKey = await tester.Execute(request);
        var stored = await GetStoredObject(tester, "SomEthinG1", storageKey);
        Assert.That(stored, Is.EqualTo(request.Data));
    }

    [Test]
    public async Task ShouldStoreMultipleObjects()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
        var request1 = new StoreObjectRequest
        {
            StorageName = "something",
            Data = "Whatever1"
        };
        var storageKey1 = await tester.Execute(request1);
        var request2 = new StoreObjectRequest
        {
            StorageName = "something",
            Data = "Whatever2"
        };
        var storageKey2 = await tester.Execute(request2);
        var stored1 = await GetStoredObject(tester, request1.StorageName, storageKey1);
        Assert.That(stored1, Is.EqualTo(request1.Data));
        var stored2 = await GetStoredObject(tester, request2.StorageName, storageKey2);
        Assert.That(stored2, Is.EqualTo(request2.Data));
    }

    [Test]
    public async Task ShouldReturnTheSameKey_WhenStorageNameAndDataAreTheSame()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
        var request = new StoreObjectRequest
        {
            StorageName = "something",
            Data = "Whatever1"
        };
        var storageKey1 = await tester.Execute(request);
        var storageKey2 = await tester.Execute(request);
        Assert.That(storageKey1, Is.EqualTo(storageKey2));
    }

    [Test]
    public async Task ShouldReturnDifferentKeys_WhenStorageNameIsTheSameButNotTheData()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
        var request = new StoreObjectRequest
        {
            StorageName = "something",
            Data = "Whatever1"
        };
        var storageKey1 = await tester.Execute(request);
        request.StorageName = "something else";
        var storageKey2 = await tester.Execute(request);
        Assert.That(storageKey1, Is.Not.EqualTo(storageKey2));
    }

    [Test]
    public async Task ShouldThrowError_WhenStoredObjectIsNotFound()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
        var request = new StoreObjectRequest
        {
            StorageName = "something",
            Data = "Whatever1"
        };
        var storageKey = await tester.Execute(request);
        var serializedData = await GetStoredObject(tester, "something else", storageKey);
        Assert.That(serializedData, Is.EqualTo(""), "Should return empty string when the stored object is not found");

    }

    [Test]
    public async Task ShouldReturnEmptyString_WhenStoredObjectHasExpired()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
        var clock = tester.Services.GetRequiredService<FakeClock>();
        var request = new StoreObjectRequest
        {
            StorageName = "something",
            Data = "Whatever1",
            ExpireAfter = TimeSpan.FromMinutes(1)
        };
        var storageKey = await tester.Execute(request);
        clock.Add(request.ExpireAfter.Add(TimeSpan.FromSeconds(1)));
        var serializedData = await GetStoredObject(tester, request.StorageName, storageKey);
        Assert.That(serializedData, Is.EqualTo(""), "Should return empty string when the stored object has expired");
    }

    [Test]
    public async Task ShouldGenerateSixDigitKey()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
        var request = new StoreObjectRequest
        {
            StorageName = "Something",
            Data = "Whatever",
            GenerateKey = GenerateKeyModel.SixDigit()
        };
        var storageKey = await tester.Execute(request);
        var parsed = int.TryParse(storageKey, out var _);
        Assert.That(parsed, Is.True, $"Storage key '{storageKey}' should be all digits");
        Assert.That(storageKey.Length, Is.EqualTo(6), "Should generate 6 digit key");
    }

    [Test]
    public async Task ShouldGenerateTenDigitKey()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
        var request = new StoreObjectRequest
        {
            StorageName = "Something",
            Data = "Whatever",
            GenerateKey = GenerateKeyModel.TenDigit()
        };
        var storageKey = await tester.Execute(request);
        var parsed = long.TryParse(storageKey, out var _);
        Assert.That(parsed, Is.True, $"Storage key '{storageKey}' should be all digits");
        Assert.That(storageKey.Length, Is.EqualTo(10), "Should generate 10 digit key");
    }

    [Test]
    public async Task ShouldGenerateFixedKey()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
        var request = new StoreObjectRequest
        {
            StorageName = "Something",
            Data = "Whatever",
            GenerateKey = GenerateKeyModel.Fixed("SomeKey")
        };
        var storageKey = await tester.Execute(request);
        Assert.That(storageKey, Is.EqualTo("SomeKey"), "Should generate fixed key");
    }

    [Test]
    public async Task ShouldDeleteSingleUseStoredObject()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
        var request = new StoreObjectRequest
        (
            new StorageName("something"), 
            "Whatever", 
            TimeSpan.FromMinutes(15)
        )
        .SingleUse();
        var storageKey = await tester.Execute(request);
        await GetStoredObject(tester, request.StorageName, storageKey);
        var db = tester.Services.GetRequiredService<HubDbContext>();
        var storedObjects = await db.StoredObjects.Retrieve().ToArrayAsync();
        Assert.That(storedObjects.Length, Is.EqualTo(0), "Should delete single use stored object");
    }

    [Test]
    public async Task ShouldSlideExpiration()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
        var request = new StoreObjectRequest
        (
            new StorageName("something"),
            "Whatever",
            TimeSpan.FromMinutes(15)
        )
        .SlidingExpiration();
        var storageKey = await tester.Execute(request);
        var clock = tester.Services.GetRequiredService<FakeClock>();
        clock.Add(TimeSpan.FromMinutes(5));
        await GetStoredObject(tester, request.StorageName, storageKey);
        clock.Add(TimeSpan.FromMinutes(11));
        var storedObject = await GetStoredObject(tester, request.StorageName, storageKey);
        Assert.That(storedObject, Is.Not.EqualTo(""), "Should slide expiration");
    }

    [Test]
    public async Task ShouldExpireAfterSlidingExpiration()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
        var request = new StoreObjectRequest
        (
            new StorageName("something"),
            "Whatever",
            TimeSpan.FromMinutes(15)
        )
        .SlidingExpiration();
        var storageKey = await tester.Execute(request);
        var clock = tester.Services.GetRequiredService<FakeClock>();
        clock.Add(TimeSpan.FromMinutes(5));
        await GetStoredObject(tester, request.StorageName, storageKey);
        clock.Add(TimeSpan.FromMinutes(16));
        var storedObject = await GetStoredObject(tester, request.StorageName, storageKey);
        Assert.That(storedObject, Is.EqualTo(""), "Should expire after sliding expiration");
    }

    private Task<string> GetStoredObject(IHubActionTester tester, string storageName, string storageKey)
    {
        var getStoredObj = tester.Create((hubApi) => hubApi.Storage.GetStoredObject);
        return getStoredObj.Execute
        (
            new GetStoredObjectRequest
            {
                StorageName = storageName,
                StorageKey = storageKey
            }
        );
    }

    private async Task<HubActionTester<StoreObjectRequest, string>> Setup()
    {
        var host = new HubTestHost();
        var sp = await host.Setup();
        return HubActionTester.Create(sp, hubApi => hubApi.Storage.StoreObject);
    }
}
