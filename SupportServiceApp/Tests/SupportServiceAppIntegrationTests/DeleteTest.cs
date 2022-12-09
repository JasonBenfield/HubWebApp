using XTI_App.Api;
using XTI_Core;

namespace SupportServiceAppIntegrationTests;

internal sealed class DeleteTest
{
    [Test]
    public async Task ShouldDeleteInstallationsWithPendingDeleteStatus()
    {
        var tester = Setup();
        await tester.Execute(new EmptyRequest());
    }

    private static SupportActionTester<EmptyRequest, EmptyActionResult> Setup()
    {
        var host = TestHost.CreateDefault(XtiEnvironment.Development);
        var scope = host.NewScope();
        var tester = new SupportActionTester<EmptyRequest, EmptyActionResult>
        (
            scope,
            api => api.Installations.Delete
        );
        return tester;
    }
}
