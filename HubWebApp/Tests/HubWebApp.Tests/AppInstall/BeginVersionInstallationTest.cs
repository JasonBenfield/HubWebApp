namespace HubWebApp.Tests;

sealed class BeginVersionInstallationTest
{
    [Test]
    public async Task ShouldSetVersionInstallationStatusToInstallStarted()
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
                versionKey: appVersion.Version.Key(),
                installConfigurationID: config.ID,
                installAsCurrent: true,
                isAutoStartEnabled: true
            )
        );
        var versionInstallation = await tester.Execute
        (
            new BeginInstallationRequest
            (
                commandID: requestedInstallationDetail.Command.ID,
                isCurrent: false
            )
        );
        Assert.That
        (
            versionInstallation.Status,
            Is.EqualTo(InstallStatus.Values.InstallStarted)
        );
    }

    private async Task<HubActionTester<BeginInstallationRequest, InstallationModel>> Setup()
    {
        var host = new HubTestHost();
        var sp = await host.Setup();
        return HubActionTester.Create(sp, hubApi => hubApi.Command.BeginInstallation);
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
        var modKeyAccessor = tester.Services.GetRequiredService<FakeModifierKeyAccessor>();
        modKeyAccessor.SetValue(new ModifierKey(HubInfo.AppKey.Format()));
        var hubApi = tester.Services.GetRequiredService<HubAppApiFactory>().CreateForSuperUser();
        var result = await hubApi.App.AddInstallCommand.Execute(requestData);
        return result.Data!;
    }
}