using XTI_Hub.Abstractions;

namespace HubWebApp.IntegrationTests;

internal sealed class GetLogEntryDetailTest
{
    [Test]
    public async Task ShouldGetLogEntryDetail()
    {
        var tester = await Setup();
        var logEntry = await tester.Execute(43);
        logEntry.WriteToConsole();
    }

    private async Task<HubActionTester<int, AppLogEntryDetailModel>> Setup(string envName = "Development")
    {
        var host = new HubTestHost();
        var sp = await host.Setup(envName);
        return HubActionTester.Create(sp, hubApi => hubApi.Logs.GetLogEntryDetail);
    }
}
