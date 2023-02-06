using XTI_Hub.Abstractions;

namespace HubWebApp.IntegrationTests;

internal sealed class GetLogEntryByKeyTest
{
    [Test]
    public async Task ShouldGetLogEntryByKey()
    {
        var tester = await Setup();
        var logEntry = await tester.Execute("0542aa328c9743f087b74c3b318d7ee4");
        logEntry.WriteToConsole();
    }

    private async Task<HubActionTester<string, AppLogEntryModel>> Setup(string envName = "Development")
    {
        var host = new HubTestHost();
        var sp = await host.Setup(envName);
        return HubActionTester.Create(sp, hubApi => hubApi.Logs.GetLogEntryByKey);
    }
}
