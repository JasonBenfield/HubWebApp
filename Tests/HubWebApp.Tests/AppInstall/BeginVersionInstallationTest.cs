using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using XTI_Hub;
using XTI_HubAppApi;
using XTI_HubAppApi.AppInstall;
using XTI_HubDB.Entities;

namespace HubWebApp.Tests;

sealed class BeginVersionInstallationTest
{
    [Test]
    public async Task ShouldSetVersionInstallationStatusToInstallStarted()
    {
        var tester = await setup();
        var hubApp = await tester.HubApp();
        var version = await hubApp.CurrentVersion();
        tester.LoginAsAdmin();
        const string qualifiedMachineName = "machine.example.com";
        var newInstResult = await newInstallation(tester, new NewInstallationRequest
        {
            QualifiedMachineName = qualifiedMachineName,
            AppKey = HubInfo.AppKey
        });
        var request = new BeginInstallationRequest
        {
            QualifiedMachineName = qualifiedMachineName,
            AppKey = HubInfo.AppKey,
            VersionKey = version.Key().Value
        };
        await tester.Execute(request);
        var versionInstallation = await getInstallation(tester, newInstResult.VersionInstallationID);
        Assert.That
        (
            InstallStatus.Values.Value(versionInstallation.Status),
            Is.EqualTo(InstallStatus.Values.InstallStarted),
            "Should set version installation status to install started"
        );
    }

    [Test]
    public async Task ShouldReturnVersionInstallationID()
    {
        var tester = await setup();
        var hubApp = await tester.HubApp();
        var version = await hubApp.CurrentVersion();
        tester.LoginAsAdmin();
        const string qualifiedMachineName = "machine.example.com";
        var newInstResult = await newInstallation(tester, new NewInstallationRequest
        {
            QualifiedMachineName = qualifiedMachineName,
            AppKey = HubInfo.AppKey
        });
        var request = new BeginInstallationRequest
        {
            QualifiedMachineName = qualifiedMachineName,
            AppKey = HubInfo.AppKey,
            VersionKey = version.Key().Value
        };
        var installationID = await tester.Execute(request);
        Assert.That
        (
            installationID,
            Is.EqualTo(newInstResult.VersionInstallationID),
            "Should return version install ID"
        );
    }

    private static Task<InstallationEntity> getInstallation(IHubActionTester tester, int installationID)
    {
        var db = tester.Services.GetRequiredService<IHubDbContext>();
        return db.Installations.Retrieve()
            .Where(inst => inst.ID == installationID)
            .FirstAsync();
    }

    private async Task<NewInstallationResult> newInstallation(IHubActionTester tester, NewInstallationRequest model)
    {
        var hubApi = tester.Services.GetRequiredService<HubAppApiFactory>().CreateForSuperUser();
        var result = await hubApi.Install.NewInstallation.Execute(model);
        return result.Data;
    }

    private async Task<HubActionTester<BeginInstallationRequest, int>> setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup();
        return HubActionTester.Create(services, hubApi => hubApi.Install.BeginVersionInstallation);
    }
}