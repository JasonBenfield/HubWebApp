using XTI_Admin;
using XTI_Core;
using XTI_Hub.Abstractions;

namespace HubWebApp.IntegrationTests;

internal sealed class GetStoredObjectTest
{
    [Test]
    public async Task ShouldGetStoredObject()
    {
        var tester = await Setup("Production");
        var serializedAdminOptions = await tester.Execute(new GetStoredObjectRequest(new StorageName("xtiadminoptions"), "379233"));
        serializedAdminOptions.WriteToConsole();
        var adminOptions = XtiSerializer.Deserialize<AdminOptions>(serializedAdminOptions);
        adminOptions.WriteToConsole();
    }

    private async Task<HubActionTester<GetStoredObjectRequest, string>> Setup(string envName = "Development")
    {
        var host = new HubTestHost();
        var sp = await host.Setup(envName);
        return HubActionTester.Create(sp, hubApi => hubApi.Storage.GetStoredObject);
    }
}
