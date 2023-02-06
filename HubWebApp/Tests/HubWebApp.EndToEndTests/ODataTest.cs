using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using XTI_Hub;
using XTI_HubAppClient;
using XTI_HubAppClient.Extensions;

namespace HubWebApp.EndToEndTests;

internal sealed class ODataTest
{
    [Test]
    public async Task ShouldGetLogEntries()
    {
        var sp = new TestHost().Setup(HubInfo.AppKey, "Development");
        var hubClient = sp.GetRequiredService<HubAppClient>();
        hubClient.UseToken<SystemUserXtiToken>();
        var logEntries = await hubClient.LogEntryQuery.Get
        (
            "",
            "$top=10",
            new LogEntryQueryRequest
            {
                RequestID = 1588081
            },
            default
        );
        logEntries.WriteToConsole();
    }

    [Test]
    public async Task ShouldGetExcelFile()
    {
        var sp = new TestHost().Setup(HubInfo.AppKey, "Development");
        var hubClient = sp.GetRequiredService<HubAppClient>();
        hubClient.UseToken<SystemUserXtiToken>();
        var fileResult = await hubClient.LogEntryQuery.ToExcel
        (
            "",
            "$select=AppKey,RequestID,SeverityText,Caption,Message",
            new LogEntryQueryRequest
            {
                RequestID = 1588081
            },
            default
        );
        fileResult.WriteToFile(TestContext.CurrentContext.WorkDirectory, "odata.xlsx", true);
    }
}
