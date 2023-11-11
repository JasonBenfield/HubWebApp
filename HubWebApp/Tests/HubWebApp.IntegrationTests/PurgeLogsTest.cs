namespace HubWebApp.IntegrationTests;

internal sealed class PurgeLogsTest
{
    [Test]
    public async Task ShouldPurgeLogs()
    {
        var tester = await Setup();
        await tester.Execute(new EmptyRequest());
    }

    private async Task<HubActionTester<EmptyRequest, EmptyActionResult>> Setup(string envName = "Development")
    {
        var host = new HubTestHost();
        var sp = await host.Setup(envName);
        return HubActionTester.Create(sp, hubApi => hubApi.Periodic.PurgeLogs);
    }
}
