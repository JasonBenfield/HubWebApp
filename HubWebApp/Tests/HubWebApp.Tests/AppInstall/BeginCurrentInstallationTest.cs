using Microsoft.EntityFrameworkCore;
using XTI_Hub.Abstractions;
using XTI_HubAppApi.AppInstall;
using XTI_HubAppApi.AppPublish;
using XTI_HubDB.Entities;

namespace HubWebApp.Tests;

sealed class BeginCurrentInstallationTest
{
    [Test]
    public async Task ShouldSetCurrentInstallationStatusToInstallStarted()
    {
        var tester = await setup();
        var factory = tester.Services.GetRequiredService<AppFactory>();
        var hubApp = await factory.Apps.App(HubInfo.AppKey);
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
            VersionKey = version.Key()
        };
        await tester.Execute(request);
        var currentInstallation = await getInstallation(tester, newInstResult.CurrentInstallationID);
        Assert.That
        (
            InstallStatus.Values.Value(currentInstallation.Status),
            Is.EqualTo(InstallStatus.Values.InstallStarted),
            "Should set current installation status to install started"
        );
    }

    [Test]
    public async Task ShouldReturnInstallationID()
    {
        var tester = await setup();
        var factory = tester.Services.GetRequiredService<AppFactory>();
        var hubApp = await factory.Apps.App(HubInfo.AppKey);
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
            VersionKey = version.Key()
        };
        var installationID = await tester.Execute(request);
        Assert.That
        (
            installationID,
            Is.EqualTo(newInstResult.CurrentInstallationID),
            "Should return current installation ID"
        );
    }

    [Test]
    public async Task ShouldSetCurrentInstallationVersion()
    {
        var tester = await setup();
        var factory = tester.Services.GetRequiredService<AppFactory>();
        tester.LoginAsAdmin();
        const string qualifiedMachineName = "machine.example.com";
        await newInstallation(tester, new NewInstallationRequest
        {
            QualifiedMachineName = qualifiedMachineName,
            AppKey = HubInfo.AppKey
        });
        var hubApiFactory = tester.Services.GetRequiredService<HubAppApiFactory>();
        var hubApi = hubApiFactory.CreateForSuperUser();
        var nextVersion = await hubApi.Publish.NewVersion.Invoke
        (
            new NewVersionRequest
            {
                VersionName = "HubWebApp",
                VersionType = AppVersionType.Values.Major,
                AppDefinitions = new[] { new AppDefinitionModel(HubInfo.AppKey, "hub.example.com") }
            }
        );
        await hubApi.Publish.BeginPublish.Invoke
        (
            new PublishVersionRequest
            {
                VersionName = nextVersion.VersionName,
                VersionKey = nextVersion.VersionKey
            }
        );
        await hubApi.Install.RegisterApp.Invoke(new RegisterAppRequest
        {
            AppTemplate = hubApiFactory.CreateTemplate().ToModel(),
            VersionKey = nextVersion.VersionKey,
            Versions = new XtiVersionModel[0]
        });
        await hubApi.Publish.EndPublish.Invoke
        (
            new PublishVersionRequest
            {
                VersionName = nextVersion.VersionName,
                VersionKey = nextVersion.VersionKey
            }
        );
        await newInstallation(tester, new NewInstallationRequest
        {
            QualifiedMachineName = qualifiedMachineName,
            AppKey = HubInfo.AppKey
        });
        var request = new BeginInstallationRequest
        {
            QualifiedMachineName = qualifiedMachineName,
            AppKey = HubInfo.AppKey,
            VersionKey = nextVersion.VersionKey
        };
        var installationID = await tester.Execute(request);
        var currentInstallation = await getInstallation(tester, installationID);
        var installationVersion = await getVersion(tester, currentInstallation);
        Assert.That
        (
            installationVersion.ID,
            Is.EqualTo(nextVersion.ID),
            "Should set current installation version"
        );
    }

    private static Task<InstallationEntity> getInstallation(IHubActionTester tester, int installationID)
    {
        var db = tester.Services.GetRequiredService<IHubDbContext>();
        return db.Installations.Retrieve()
            .Where(inst => inst.ID == installationID)
            .FirstAsync();
    }

    private static Task<AppXtiVersionEntity> getVersion(IHubActionTester tester, InstallationEntity installation)
    {
        var db = tester.Services.GetRequiredService<IHubDbContext>();
        return db.AppVersions.Retrieve()
            .Where(av => av.ID == installation.AppVersionID)
            .FirstAsync();
    }

    private Task<NewInstallationResult> newInstallation(IHubActionTester tester, NewInstallationRequest model)
    {
        var hubApi = tester.Services.GetRequiredService<HubAppApiFactory>().CreateForSuperUser();
        return hubApi.Install.NewInstallation.Invoke(model);
    }

    private async Task<HubActionTester<BeginInstallationRequest, int>> setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup();
        return HubActionTester.Create(services, hubApi => hubApi.Install.BeginCurrentInstallation);
    }
}