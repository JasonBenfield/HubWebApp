using Microsoft.EntityFrameworkCore;
using XTI_Hub.Abstractions;
using XTI_HubWebAppApi.AppInstall;
using XTI_HubDB.Entities;

namespace HubWebApp.Tests;

sealed class InstalledTest
{
    [Test]
    public async Task ShouldSetCurrentInstallationStatusToInstalled()
    {
        var tester = await setup();
        var hubApp = await tester.HubApp();
        var version = await hubApp.CurrentVersion();
        await tester.LoginAsAdmin();
        const string qualifiedMachineName = "machine.example.com";
        var newInstResult = await newInstallation(tester, new NewInstallationRequest
        {
            VersionName = version.ToVersionModel().VersionName,
            QualifiedMachineName = qualifiedMachineName,
            AppKey = HubInfo.AppKey
        });
        await startInstallation(tester, new InstallationRequest
        {
            InstallationID = newInstResult.CurrentInstallationID
        });
        await tester.Execute
        (
            new InstallationRequest(newInstResult.CurrentInstallationID)
        );
        var currentInstallation = await getInstallation(tester, newInstResult.CurrentInstallationID);
        Assert.That
        (
            InstallStatus.Values.Value(currentInstallation?.Status ?? 0),
            Is.EqualTo(InstallStatus.Values.Installed),
            "Should set current installation status to Installed"
        );
    }

    [Test]
    public async Task ShouldSetVersionInstallationStatusToInstalled()
    {
        Environment.SetEnvironmentVariable("DOTNET_ENVIRONMENT", "Production");
        var tester = await setup();
        var hubApp = await tester.HubApp();
        var version = await hubApp.CurrentVersion();
        await tester.LoginAsAdmin();
        const string qualifiedMachineName = "machine.example.com";
        var newInstResult = await newInstallation(tester, new NewInstallationRequest
        {
            VersionName = version.ToVersionModel().VersionName,
            QualifiedMachineName = qualifiedMachineName,
            AppKey = HubInfo.AppKey
        });
        await startInstallation(tester, new InstallationRequest
        {
            InstallationID = newInstResult.VersionInstallationID
        });
        await tester.Execute
        (
            new InstallationRequest(newInstResult.VersionInstallationID)
        );
        var versionInstallation = await getInstallation(tester, newInstResult.VersionInstallationID);
        Assert.That
        (
            InstallStatus.Values.Value(versionInstallation?.Status ?? 0),
            Is.EqualTo(InstallStatus.Values.Installed),
            "Should set version installation status to Installed"
        );
    }

    [Test]
    public async Task ShouldDeletePreviousCurrentInstallation()
    {
        var tester = await setup();
        var hubApp = await tester.HubApp();
        var version = await hubApp.CurrentVersion();
        await tester.LoginAsAdmin();
        const string qualifiedMachineName = "machine.example.com";
        var newInstResult1 = await newInstallation(tester, new NewInstallationRequest
        {
            VersionName = version.ToVersionModel().VersionName,
            QualifiedMachineName = qualifiedMachineName,
            AppKey = HubInfo.AppKey
        });
        var newInstResult2 = await newInstallation(tester, new NewInstallationRequest
        {
            VersionName = version.ToVersionModel().VersionName,
            QualifiedMachineName = qualifiedMachineName,
            AppKey = HubInfo.AppKey
        });
        await startInstallation(tester, new InstallationRequest
        {
            InstallationID = newInstResult2.CurrentInstallationID
        });
        await tester.Execute
        (
            new InstallationRequest(newInstResult2.CurrentInstallationID)
        );
        var factory = tester.Services.GetRequiredService<HubFactory>();
        var installation1 = await factory.Installations.InstallationOrDefault(newInstResult1.CurrentInstallationID);
        Assert.That(installation1.ToModel().Status, Is.EqualTo(InstallStatus.Values.Deleted), "Should delete previous current installation");
    }

    [Test]
    public async Task ShouldNotDeletePreviousCurrentInstallationOfADifferentApp()
    {
        var tester = await setup();
        var hubApp = await tester.HubApp();
        var version = await hubApp.CurrentVersion();
        await tester.LoginAsAdmin();
        const string qualifiedMachineName = "machine.example.com";
        var fakeApp = await registerFakeApp(tester);
        var fakeVersion = await fakeApp.CurrentVersion();
        var newInstResult1 = await newInstallation(tester, new NewInstallationRequest
        {
            VersionName = fakeVersion.ToVersionModel().VersionName,
            QualifiedMachineName = qualifiedMachineName,
            AppKey = FakeInfo.AppKey
        });
        var newInstResult2 = await newInstallation(tester, new NewInstallationRequest
        {
            VersionName = version.ToVersionModel().VersionName,
            QualifiedMachineName = qualifiedMachineName,
            AppKey = HubInfo.AppKey
        });
        await startInstallation(tester, new InstallationRequest
        {
            InstallationID = newInstResult2.CurrentInstallationID
        });
        await tester.Execute
        (
            new InstallationRequest(newInstResult2.CurrentInstallationID)
        );
        var factory = tester.Services.GetRequiredService<HubFactory>();
        var installation1 = await factory.Installations.InstallationOrDefault(newInstResult1.CurrentInstallationID);
        Assert.That(installation1.ToModel().Status, Is.EqualTo(InstallStatus.Values.InstallPending), "Should not delete previous current installation of a different app");
    }

    private Task<App> registerFakeApp(IHubActionTester tester)
    {
        var factory = tester.Services.GetRequiredService<HubFactory>();
        return factory.Apps.AddOrUpdate(new AppVersionName("fake"), FakeInfo.AppKey, DateTimeOffset.Now);
    }

    private async Task<HubActionTester<InstallationRequest, EmptyActionResult>> setup()
    {
        var host = new HubTestHost();
        var sp = await host.Setup();
        return HubActionTester.Create(sp, hubApi => hubApi.Install.Installed);
    }

    private async Task<NewInstallationResult> newInstallation(IHubActionTester tester, NewInstallationRequest model)
    {
        var hubApi = tester.Services.GetRequiredService<HubAppApiFactory>().CreateForSuperUser();
        var result = await hubApi.Install.NewInstallation.Execute(model);
        return result.Data;
    }

    private Task startInstallation(IHubActionTester tester, InstallationRequest model)
    {
        var hubApi = tester.Services.GetRequiredService<HubAppApiFactory>().CreateForSuperUser();
        return hubApi.Install.BeginInstallation.Execute(model);
    }

    private static Task<InstallationEntity> getInstallation(IHubActionTester tester, int installationID)
    {
        var db = tester.Services.GetRequiredService<IHubDbContext>();
        return db.Installations.Retrieve()
            .Where(inst => inst.ID == installationID)
            .FirstAsync();
    }

}