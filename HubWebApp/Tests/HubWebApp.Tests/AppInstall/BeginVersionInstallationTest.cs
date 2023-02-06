using Microsoft.EntityFrameworkCore;
using XTI_HubDB.Entities;
using XTI_HubWebAppApi.AppInstall;

namespace HubWebApp.Tests;

sealed class BeginVersionInstallationTest
{
    [Test]
    public async Task ShouldSetVersionInstallationStatusToInstallStarted()
    {
        var tester = await Setup();
        var hubApp = await tester.HubApp();
        var appVersion = await hubApp.CurrentVersion();
        await tester.LoginAsAdmin();
        const string qualifiedMachineName = "machine.example.com";
        var newInstResult = await NewInstallation(tester, new NewInstallationRequest
        {
            VersionName = appVersion.Version.ToModel().VersionName,
            QualifiedMachineName = qualifiedMachineName,
            AppKey = HubInfo.AppKey
        });
        await tester.Execute(new GetInstallationRequest(newInstResult.VersionInstallationID));
        var versionInstallation = await GetInstallation(tester, newInstResult.VersionInstallationID);
        Assert.That
        (
            InstallStatus.Values.Value(versionInstallation.Status),
            Is.EqualTo(InstallStatus.Values.InstallStarted),
            "Should set version installation status to install started"
        );
    }

    private async Task<HubActionTester<GetInstallationRequest, EmptyActionResult>> Setup()
    {
        var host = new HubTestHost();
        var sp = await host.Setup();
        return HubActionTester.Create(sp, hubApi => hubApi.Install.BeginInstallation);
    }

    private async Task<NewInstallationResult> NewInstallation(IHubActionTester tester, NewInstallationRequest model)
    {
        var hubApi = tester.Services.GetRequiredService<HubAppApiFactory>().CreateForSuperUser();
        var result = await hubApi.Install.NewInstallation.Execute(model);
        return result.Data!;
    }

    private static Task<InstallationEntity> GetInstallation(IHubActionTester tester, int installationID)
    {
        var db = tester.Services.GetRequiredService<IHubDbContext>();
        return db.Installations.Retrieve()
            .Where(inst => inst.ID == installationID)
            .FirstAsync();
    }
}