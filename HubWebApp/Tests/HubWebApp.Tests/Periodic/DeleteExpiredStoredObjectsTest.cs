using Microsoft.EntityFrameworkCore;
using XTI_Core;
using XTI_Core.Fakes;
using XTI_HubDB.EF;

namespace HubWebApp.Tests;

internal sealed class DeleteExpiredStoredObjectsTest
{
    [Test]
    public async Task ShouldDeleteExpiredStoredObject()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
        var expiresAfter = TimeSpan.FromMinutes(15);
        await StoreObject(tester, new StorageName("Test"), new { Test = "Test" }, expiresAfter);
        var clock = tester.Services.GetRequiredService<FakeClock>();
        clock.Add(expiresAfter.Add(TimeSpan.FromSeconds(1)));
        await tester.Execute(new EmptyRequest());
        var db = tester.Services.GetRequiredService<HubDbContext>();
        var storedObjects = await db.StoredObjects.Retrieve().ToArrayAsync();
        Assert.That(storedObjects.Length, Is.EqualTo(0));
    }

    [Test]
    public async Task ShouldNotDeleteStoredObjectBeforeExpiration()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
        var expiresAfter = TimeSpan.FromMinutes(15);
        await StoreObject(tester, new StorageName("Test"), new { Test = "Test" }, expiresAfter);
        var clock = tester.Services.GetRequiredService<FakeClock>();
        clock.Add(expiresAfter.Subtract(TimeSpan.FromSeconds(1)));
        await tester.Execute(new EmptyRequest());
        var db = tester.Services.GetRequiredService<HubDbContext>();
        var storedObjects = await db.StoredObjects.Retrieve().ToArrayAsync();
        Assert.That(storedObjects.Length, Is.EqualTo(1));
    }

    private async Task<HubActionTester<EmptyRequest, EmptyActionResult>> Setup()
    {
        var host = new HubTestHost();
        var sp = await host.Setup();
        var hubFactory = sp.GetRequiredService<HubFactory>();
        var clock = sp.GetRequiredService<IClock>();
        var apiFactory = sp.GetRequiredService<HubAppApiFactory>();
        var hubApi = apiFactory.CreateForSuperUser();
        return HubActionTester.Create(sp, hubApi => hubApi.Periodic.DeleteExpiredStoredObjects);
    }

    private Task<string> StoreObject(IHubActionTester tester, StorageName storageName, object data, TimeSpan expireAfter)
    {
        var storeTester = tester.Create(api => api.Storage.StoreObject);
        return storeTester.Execute
        (
            new StoreObjectRequest
            (
                storageName: storageName,
                data: XtiSerializer.Serialize(data),
                expireAfter: expireAfter,
                GenerateKeyModel.SixDigit()
            )
        );
    }

}