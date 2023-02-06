using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using XTI_App.Abstractions;
using XTI_Hub;
using XTI_Hub.Abstractions;
using XTI_HubAppClient;

namespace HubWebApp.EndToEndTests;

internal sealed class EndToEndTest
{
    [Test]
    public async Task ShouldLogin()
    {
        var sp = new TestHost().Setup(HubInfo.AppKey);
        var addUserModel = new AddOrUpdateUserRequest
        {
            UserName = NewUserXtiToken.NewUserCredentials.UserName,
            Password = NewUserXtiToken.NewUserCredentials.Password
        };
        var hubClient = sp.GetRequiredService<HubAppClient>();
        await hubClient.Users.AddOrUpdateUser("General", addUserModel);
        hubClient.UseToken<NewUserXtiToken>();
        var ex = Assert.ThrowsAsync<AppClientException>
        (
            async () =>
            {
                await hubClient.Users.AddOrUpdateUser
                (
                    "General", 
                    new AddOrUpdateUserRequest
                    {
                        UserName = "TestUser2",
                        Password = "Password12345"
                    }
                );
            }
        );
        Assert.That(ex?.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Forbidden));
        Console.WriteLine(ex);

    }
}