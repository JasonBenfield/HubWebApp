using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XTI_App.Abstractions;
using XTI_Core;
using XTI_Hub.Abstractions;
using XTI_HubAppClient;
using XTI_HubAppClient.Extensions;

namespace HubWebApp.EndToEndTests;

internal sealed class SystemTest
{
    [Test]
    public async Task ShouldGetAppContext()
    {
        var sp = new TestHost().Setup(AppKey.WebApp("Test"), "Development");
        var hubClient = sp.GetRequiredService<HubAppClient>();
        hubClient.UseToken<SystemUserXtiToken>();
        var appContext = await hubClient.System.GetAppContext
        (
            new GetAppContextRequest(installationID: 1190)
        );
        appContext.WriteToConsole();
    }

    [Test]
    public async Task ShouldGetUserAppContext()
    {
        var sp = new TestHost().Setup(AppKey.WebApp("Test"), "Development");
        var hubClient = sp.GetRequiredService<HubAppClient>();
        hubClient.UseToken<SystemUserXtiToken>();
        var userContext = await hubClient.System.GetUserContext
        (
            new GetUserContextRequest(installationID: 1190, userName: new AppUserName("JB"))
        );
        userContext.WriteToConsole();
    }

    [Test]
    public async Task ShouldGetUsersWithAnyRole()
    {
        var sp = new TestHost().Setup(AppKey.WebApp("Test"), "Development");
        var hubClient = sp.GetRequiredService<HubAppClient>();
        hubClient.UseToken<SystemUserXtiToken>();
        var users = await hubClient.System.GetUsersWithAnyRole
        (
            new SystemGetUsersWithAnyRoleRequest
            (
                installationID: 1190,
                roleNames: AppRoleName.Admin
            )
        );
        users.WriteToConsole();
    }

    [Test]
    public async Task ShouldGetUserOrAnon()
    {
        var sp = new TestHost().Setup(AppKey.WebApp("Test"), "Development");
        var hubClient = sp.GetRequiredService<HubAppClient>();
        hubClient.UseToken<SystemUserXtiToken>();
        var user = await hubClient.System.GetUserOrAnon("JB");
        user.WriteToConsole();
        var notAUser = await hubClient.System.GetUserOrAnon("not_a_user");
        notAUser.WriteToConsole();
    }

    [Test]
    public async Task ShouldStoreObject()
    {
        var sp = new TestHost().Setup(AppKey.WebApp("Test"), "Development");
        var hubClient = sp.GetRequiredService<HubAppClient>();
        hubClient.UseToken<SystemUserXtiToken>();
        var storageName = new StorageName("Test Stored Object");
        var originalStoredObject = new AppUserModel(1, new AppUserName("TEST"), new PersonName("Test"), "", DateTimeOffset.MaxValue);
        var data = XtiSerializer.Serialize(originalStoredObject);
        var storageKey = await hubClient.System.StoreObject
        (
            new StoreObjectRequest
            (
                storageName: storageName,
                data: data,
                expireAfter: TimeSpan.FromMinutes(30),
                GenerateKeyModel.SixDigit()
            )
        );
        Console.WriteLine($"storageKey: {storageKey}");
        var serializedStoredObject = await hubClient.System.GetStoredObject
        (
            new GetStoredObjectRequest(storageName: storageName, storageKey: storageKey)
        );
        var deserializedStoredObject = XtiSerializer.Deserialize<AppUserModel>(serializedStoredObject);
        Assert.That(deserializedStoredObject, Is.EqualTo(originalStoredObject));
    }

    [Test]
    public async Task ShouldGetUserAuthenticators()
    {
        var sp = new TestHost().Setup(AppKey.WebApp("Test"), "Development");
        var hubClient = sp.GetRequiredService<HubAppClient>();
        hubClient.UseToken<SystemUserXtiToken>();
        var userAuthenticators = await hubClient.System.GetUserAuthenticators(2);
        userAuthenticators.WriteToConsole();
    }

    [Test]
    public async Task ShouldAddModifierByModKey()
    {
        var sp = new TestHost().Setup(AppKey.WebApp("Test"), "Development");
        var hubClient = sp.GetRequiredService<HubAppClient>();
        hubClient.UseToken<SystemUserXtiToken>();
        var modifier = await hubClient.System.AddOrUpdateModifierByModKey
        (
            new SystemAddOrUpdateModifierByModKeyRequest
            (
                installationID: 0,
                modCategoryName: new ModifierCategoryName("Department"),
                modKey: new ModifierKey("IT"),
                targetKey: "16",
                targetDisplayText: "Information Technology"
            )
        );
        modifier.WriteToConsole();
    }

    [Test]
    public async Task ShouldAddModifierByTargetKey()
    {
        var sp = new TestHost().Setup(AppKey.WebApp("Test"), "Development");
        var hubClient = sp.GetRequiredService<HubAppClient>();
        hubClient.UseToken<SystemUserXtiToken>();
        var modifier = await hubClient.System.AddOrUpdateModifierByTargetKey
        (
            new SystemAddOrUpdateModifierByTargetKeyRequest
            (
                installationID: 0,
                modCategoryName: new ModifierCategoryName("Department"),
                generateModKey: GenerateKeyModel.TenDigit(),
                targetKey: "18",
                targetDisplayText: "GIS"
            )
        );
        modifier.WriteToConsole();
    }
}
