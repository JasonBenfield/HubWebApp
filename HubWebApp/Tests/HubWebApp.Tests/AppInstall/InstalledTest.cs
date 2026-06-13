using Microsoft.EntityFrameworkCore;
using XTI_HubDB.Entities;

namespace HubWebApp.Tests;

sealed class InstalledTest
{
    [Test]
    public async Task ShouldSetCurrentInstallationStatusToInstalled()
    {
        var tester = await Setup();
        var hubApp = await tester.HubApp();
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
        var currentInstallation = await StartInstallation(tester, new BeginInstallationRequest(requestedInstallationDetail.Command.ID, true));
        await tester.Execute
        (
            new InstallationIDRequest(currentInstallation.ID)
        );
        var currentInstallationEntity = await GetInstallation(tester, currentInstallation.ID);
        Assert.That
        (
            InstallStatus.Values.Value(currentInstallationEntity?.Status ?? 0),
            Is.EqualTo(InstallStatus.Values.Installed)
        );
    }

    [Test]
    public async Task ShouldSetVersionInstallationStatusToInstalled()
    {
        Environment.SetEnvironmentVariable("DOTNET_ENVIRONMENT", "Production");
        var tester = await Setup();
        var hubApp = await tester.HubApp();
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
        var versionInstallation = await StartInstallation(tester, new BeginInstallationRequest(requestedInstallationDetail.Command.ID, false));
        await tester.Execute
        (
            new InstallationIDRequest(versionInstallation.ID)
        );
        var versionInstallationEntity = await GetInstallation(tester, versionInstallation.ID);
        Assert.That
        (
            InstallStatus.Values.Value(versionInstallationEntity?.Status ?? 0),
            Is.EqualTo(InstallStatus.Values.Installed)
        );
    }

    [Test]
    public async Task ShouldDeletePreviousCurrentInstallation()
    {
        var tester = await Setup();
        var hubApp = await tester.HubApp();
        var appVersion = await hubApp.CurrentVersion(ct: default);
        await tester.LoginAsAdmin();
        const string qualifiedMachineName = "machine.example.com";
        var config = await AddDefaultConfiguration(tester, qualifiedMachineName);
        var requestedInstallationDetail1 = await AddInstallCommand
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
        var installation1 = await StartInstallation(tester, new BeginInstallationRequest(requestedInstallationDetail1.Command.ID, true));
        var requestedInstallationDetail2 = await AddInstallCommand
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
        var installation2 = await StartInstallation(tester, new BeginInstallationRequest(requestedInstallationDetail2.Command.ID, true));
        await tester.Execute
        (
            new InstallationIDRequest(installation2.ID)
        );
        var factory = tester.Services.GetRequiredService<EfHubDB>();
        var efInstallation1 = await factory.Installations.InstallationOrDefault(installation1.ID, ct: default);
        Assert.That(efInstallation1.ToModel().Status, Is.EqualTo(InstallStatus.Values.Deleted));
    }

    [Test]
    public async Task ShouldNotDeletePreviousCurrentInstallationOfADifferentApp()
    {
        var tester = await Setup();
        var hubApp = await tester.HubApp();
        var appVersion = await hubApp.CurrentVersion(ct: default);
        await tester.LoginAsAdmin();
        const string qualifiedMachineName = "machine.example.com";
        var fakeApp = await RegisterFakeApp(tester);
        var fakeVersion = await fakeApp.CurrentVersion(ct: default);
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
        var config1 = await AddConfiguration
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
        var config2 = await AddConfiguration
        (
            tester,
            new ConfigureInstallRequest
            (
                repoOwner: "JasonBenfield",
                repoName: "Fake",
                configurationName: "Default",
                appKey: fakeApp.GetAppKey(),
                templateName: "Default",
                installSequence: 0
            )
        );
        var requestedInstallationDetail1 = await AddInstallCommand
        (
            tester,
            new AddInstallCommandRequest
            (
                appKey: HubInfo.AppKey,
                versionKey: appVersion.Version.Key(),
                installConfigurationID: config1.ID,
                installAsCurrent: true,
                isAutoStartEnabled: true
            )
        );
        var installation1 = await StartInstallation(tester, new BeginInstallationRequest(requestedInstallationDetail1.Command.ID, true));
        var requestedInstallationDetail2 = await AddInstallCommand
        (
            tester,
            new AddInstallCommandRequest
            (
                appKey: fakeApp.GetAppKey(),
                versionKey: appVersion.Version.Key(),
                installConfigurationID: config1.ID,
                installAsCurrent: true,
                isAutoStartEnabled: true
            )
        );
        var installation2 = await StartInstallation(tester, new BeginInstallationRequest(requestedInstallationDetail2.Command.ID, true));
        await tester.Execute
        (
            new InstallationIDRequest(installation2.ID)
        );
        var factory = tester.Services.GetRequiredService<EfHubDB>();
        var efInstallation1 = await factory.Installations.InstallationOrDefault(installation1.ID, ct: default);
        Assert.That(efInstallation1.ToModel().Status, Is.EqualTo(InstallStatus.Values.InstallStarted));
    }

    private Task<EfApp> RegisterFakeApp(IHubActionTester tester)
    {
        var factory = tester.Services.GetRequiredService<EfHubDB>();
        return factory.Apps.AddOrUpdate(new AppVersionName("fake"), "Fake", "Fak", FakeInfo.AppKey, DateTimeOffset.Now, ct: default);
    }

    private async Task<HubActionTester<InstallationIDRequest, EmptyActionResult>> Setup()
    {
        var host = new HubTestHost();
        var sp = await host.Setup();
        return HubActionTester.Create(sp, hubApi => hubApi.Installation.Installed);
    }

    private async Task<InstallConfigurationModel> AddDefaultConfiguration(IHubActionTester tester, string qualifiedMachineName)
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

    private async Task<InstallationModel> StartInstallation(IHubActionTester tester, BeginInstallationRequest requestData)
    {
        var hubApi = tester.Services.GetRequiredService<HubAppApiFactory>().CreateForSuperUser();
        var resultData = await hubApi.Command.BeginInstallation.Execute(requestData);
        return resultData.Data!;
    }

    private static Task<InstallationEntity> GetInstallation(IHubActionTester tester, int installationID)
    {
        var db = tester.Services.GetRequiredService<HubDbContext>();
        return db.Installations.Retrieve()
            .Where(inst => inst.ID == installationID)
            .FirstAsync();
    }

}