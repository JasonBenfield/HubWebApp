using Microsoft.EntityFrameworkCore;
using XTI_HubDB.EF.SqlServer;
using XTI_HubDB.Entities;

namespace HubWebApp.Tests;

sealed class BeginCurrentInstallationTest
{
    [Test]
    public async Task ShouldSetCurrentInstallationStatusToInstallStarted()
    {
        var tester = await Setup();
        var factory = tester.Services.GetRequiredService<EfHubDB>();
        var hubApp = await factory.Apps.App(HubInfo.AppKey, ct: default);
        var appVersion = await hubApp.CurrentVersion(ct: default);
        await tester.LoginAsAdmin();
        const string qualifiedMachineName = "machine.example.com";
        var config = await AddDefaultConfiguration(tester, qualifiedMachineName);
        var requestedInstallationDetail = await AddInstallCommand
        (
            tester,
            new AddInstallCommandRequest
            (
                appKey: HubInfo.AppKey,
                versionKey: appVersion.Version.Key(),
                installConfigurationID: config.ID,
                installAsCurrent: true,
                isAutoStartEnabled: true
            )
        );
        var currentInstallation = await tester.Execute
        (
            new BeginInstallationRequest
            (
                commandID: requestedInstallationDetail.Command.ID,
                isCurrent: true
            )
        );
        Assert.That
        (
            currentInstallation.Status,
            Is.EqualTo(InstallStatus.Values.InstallStarted)
        );
    }

    [Test]
    public async Task ShouldSetCurrentInstallationVersion()
    {
        var tester = await Setup();
        var factory = tester.Services.GetRequiredService<EfHubDB>();
        var hubApp = await factory.Apps.App(HubInfo.AppKey, ct: default);
        var appVersion = await hubApp.CurrentVersion(ct: default);
        await tester.LoginAsAdmin();
        const string qualifiedMachineName = "machine.example.com";
        var config = await AddDefaultConfiguration(tester, qualifiedMachineName);
        var installCommandDetail = await AddInstallCommand
        (
            tester,
            new AddInstallCommandRequest
            (
                appKey: HubInfo.AppKey,
                versionKey: appVersion.Version.Key(),
                installConfigurationID: config.ID,
                installAsCurrent: true,
                isAutoStartEnabled: true
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
        var newInstallCommandDetail = await AddInstallCommand
        (
            tester,
            new AddInstallCommandRequest
            (
                appKey: HubInfo.AppKey,
                versionKey: nextVersion.VersionKey,
                installConfigurationID: config.ID,
                installAsCurrent: true,
                isAutoStartEnabled: true
            )
        );
        var currentInstallation = await tester.Execute
        (
            new BeginInstallationRequest
            (
                commandID: newInstallCommandDetail.Command.ID,
                isCurrent: true
            )
        );
        var installationVersion = await GetVersion(tester, currentInstallation);
        Assert.That
        (
            installationVersion.VersionID,
            Is.EqualTo(nextVersion.ID)
        );
    }

    private static async Task<AppXtiVersionEntity> GetVersion(IHubActionTester tester, InstallationModel installation)
    {
        var db = tester.Services.GetRequiredService<HubDbContext>();
        var installationEntity = await db.Installations.Retrieve()
            .Where(inst => inst.ID == installation.ID)
            .FirstAsync();
        var appVersion = await db.AppVersions.Retrieve()
            .Where(av => av.ID == installationEntity.AppVersionID)
            .FirstAsync();
        return appVersion;
    }

    private async Task<HubActionTester<BeginInstallationRequest, InstallationModel>> Setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup();
        return HubActionTester.Create(services, hubApi => hubApi.Installations.BeginInstallation);
    }

    private async Task<InstallConfigurationModel> AddDefaultConfiguration(HubActionTester<BeginInstallationRequest, InstallationModel> tester, string qualifiedMachineName)
    {
        var configTemplate = await AddConfigurationTemplate
        (
            tester,
            new ConfigureInstallTemplateRequest
            (
                templateName: "Default",
                destinationMachineName: qualifiedMachineName,
                domain: "",
                siteName: ""
            )
        );
        var config = await AddConfiguration
        (
            tester,
            new ConfigureInstallRequest
            (
                repoOwner: "JasonBenfield",
                repoName: "Fake",
                configurationName: "Default",
                appKey: HubInfo.AppKey,
                templateName: "Default",
                installSequence: 0
            )
        );
        return config;
    }

    private async Task<InstallConfigurationTemplateModel> AddConfigurationTemplate(IHubActionTester tester, ConfigureInstallTemplateRequest configRequest)
    {
        var hubApi = tester.Services.GetRequiredService<HubAppApiFactory>().CreateForSuperUser();
        var result = await hubApi.InstallTemplates.ConfigureInstallTemplate.Execute(configRequest);
        return result.Data!;
    }

    private async Task<InstallConfigurationModel> AddConfiguration(IHubActionTester tester, ConfigureInstallRequest configRequest)
    {
        var hubApi = tester.Services.GetRequiredService<HubAppApiFactory>().CreateForSuperUser();
        var result = await hubApi.Install.ConfigureInstall.Execute(configRequest);
        return result.Data!;
    }

    private async Task<AppInstallCommandDetailModel> AddInstallCommand(IHubActionTester tester, AddInstallCommandRequest requestData)
    {
        var hubApi = tester.Services.GetRequiredService<HubAppApiFactory>().CreateForSuperUser();
        var result = await hubApi.Installations.AddInstallCommand.Execute(requestData);
        return result.Data!;
    }
}