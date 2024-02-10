using Microsoft.EntityFrameworkCore;
using XTI_Hub.Abstractions;
using XTI_HubWebAppApi.AppInstall;
using XTI_HubWebAppApi.AppPublish;
using XTI_HubDB.Entities;

namespace HubWebApp.Tests;

sealed class BeginCurrentInstallationTest
{
    [Test]
    public async Task ShouldSetCurrentInstallationStatusToInstallStarted()
    {
        var tester = await Setup();
        var factory = tester.Services.GetRequiredService<HubFactory>();
        var hubApp = await factory.Apps.App(HubInfo.AppKey);
        var appVersion = await hubApp.CurrentVersion();
        await tester.LoginAsAdmin();
        const string qualifiedMachineName = "machine.example.com";
        var newInstResult = await NewInstallation
        (
            tester,
            new NewInstallationRequest
            (
                versionName: appVersion.Version.ToModel().VersionName,
                appKey: HubInfo.AppKey,
                qualifiedMachineName: qualifiedMachineName,
                domain: "",
                siteName: ""
            )
        );
        await tester.Execute(new GetInstallationRequest(newInstResult.CurrentInstallationID));
        var currentInstallation = await GetInstallation(tester, newInstResult.CurrentInstallationID);
        Assert.That
        (
            InstallStatus.Values.Value(currentInstallation.Status),
            Is.EqualTo(InstallStatus.Values.InstallStarted),
            "Should set current installation status to install started"
        );
    }

    [Test]
    public async Task ShouldSetCurrentInstallationVersion()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
        const string qualifiedMachineName = "machine.example.com";
        await NewInstallation
        (
            tester,
            new NewInstallationRequest
            (
                versionName: new AppVersionName("HubWebApp"),
                appKey: HubInfo.AppKey,
                qualifiedMachineName: qualifiedMachineName,
                domain: "",
                siteName: ""
            )
        );
        var hubApiFactory = tester.Services.GetRequiredService<HubAppApiFactory>();
        var hubApi = hubApiFactory.CreateForSuperUser();
        var nextVersion = await hubApi.Publish.NewVersion.Invoke
        (
            new NewVersionRequest
            (
                versionName: new AppVersionName("HubWebApp"),
                versionType: AppVersionType.Values.Major
            )
        );
        await hubApi.Publish.BeginPublish.Invoke
        (
            new PublishVersionRequest
            (
                versionName: nextVersion.VersionName,
                versionKey: nextVersion.VersionKey
            )
        );
        await hubApi.Install.RegisterApp.Invoke
        (
            new RegisterAppRequest
            (
                appTemplate: hubApiFactory.CreateTemplate().ToModel(),
                versionKey: nextVersion.VersionKey
            )
        );
        await hubApi.Publish.EndPublish.Invoke
        (
            new PublishVersionRequest
            (
                versionName: nextVersion.VersionName,
                versionKey: nextVersion.VersionKey
            )
        );
        var newInstResult = await NewInstallation
        (
            tester,
            new NewInstallationRequest
            (
                versionName: nextVersion.VersionName,
                appKey: HubInfo.AppKey,
                qualifiedMachineName: qualifiedMachineName,
                domain: "",
                siteName: ""
            )
        );
        await tester.Execute(new GetInstallationRequest(newInstResult.CurrentInstallationID));
        var currentInstallation = await GetInstallation(tester, newInstResult.CurrentInstallationID);
        var installationVersion = await GetVersion(tester, currentInstallation);
        Assert.That
        (
            installationVersion.VersionID,
            Is.EqualTo(nextVersion.ID),
            "Should set current installation version"
        );
    }

    private static Task<InstallationEntity> GetInstallation(IHubActionTester tester, int installationID)
    {
        var db = tester.Services.GetRequiredService<IHubDbContext>();
        return db.Installations.Retrieve()
            .Where(inst => inst.ID == installationID)
            .FirstAsync();
    }

    private static Task<AppXtiVersionEntity> GetVersion(IHubActionTester tester, InstallationEntity installation)
    {
        var db = tester.Services.GetRequiredService<IHubDbContext>();
        return db.AppVersions.Retrieve()
            .Where(av => av.ID == installation.AppVersionID)
            .FirstAsync();
    }

    private Task<NewInstallationResult> NewInstallation(IHubActionTester tester, NewInstallationRequest model)
    {
        var hubApi = tester.Services.GetRequiredService<HubAppApiFactory>().CreateForSuperUser();
        return hubApi.Install.NewInstallation.Invoke(model);
    }

    private async Task<HubActionTester<GetInstallationRequest, EmptyActionResult>> Setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup();
        return HubActionTester.Create(services, hubApi => hubApi.Install.BeginInstallation);
    }
}