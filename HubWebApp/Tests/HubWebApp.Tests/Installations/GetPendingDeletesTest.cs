namespace HubWebApp.Tests;

internal sealed class GetPendingDeletesTest
{
    [Test]
    public async Task ShouldThrowError_WhenRoleIsNotAssignedToUser()
    {
        var tester = await Setup();
        const string qualifiedMachineName = "machine.example.com";
        await PrepareDeletePendingInstallation(tester, qualifiedMachineName);
        await AccessAssertions.Create(tester)
            .ShouldThrowError_WhenAccessIsDenied
            (
                new GetPendingCommandsRequest([AppCommandName.Delete, AppCommandName.Install], [qualifiedMachineName]),
                HubInfo.Roles.Admin,
                HubInfo.Roles.InstallationManager
            );
    }

    [Test]
    public async Task ShouldGetPendingDeletes()
    {
        var tester = await Setup();
        const string machineName = "machine.example.com";
        var deleteCommandDetail = await PrepareDeletePendingInstallation(tester, machineName);
        var commands = await tester.Execute
        (
            new GetPendingCommandsRequest
            (
                [AppCommandName.Delete, AppCommandName.Install],
                [machineName]
            )
        );
        Assert.That(commands.Length, Is.EqualTo(1));
        Assert.That
        (
            commands.Select(inst => inst.ID),
            Is.EqualTo([deleteCommandDetail.Installation.ID])
        );
    }

    [Test]
    public async Task ShouldNotIncludeInstallationsForOtherMachines()
    {
        var tester = await Setup();
        const string machineName = "machine.example.com";
        var hubApp = await tester.HubApp();
        var appVersion = await hubApp.CurrentVersion(ct: default);
        var config = await AddDefaultConfiguration(tester, "othermachine.example.com");
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
        var otherInstallation = await BeginInstallation
        (
            tester,
            new BeginInstallationRequest
            (
                commandID: installCommandDetail.Command.ID,
                isCurrent: true
            )
        );
        await Installed(tester, new InstallationIDRequest(otherInstallation.ID));
        await AddDeleteCommand(tester, new InstallationIDRequest(otherInstallation.ID));
        var commands = await tester.Execute
        (
            new GetPendingCommandsRequest
            (
                [AppCommandName.Delete, AppCommandName.Install],
                [machineName]
            )
        );
        Assert.That
        (
            commands.Length,
            Is.EqualTo(0)
        );
    }

    private async Task<HubActionTester<GetPendingCommandsRequest, AppCommandModel[]>> Setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup();
        return HubActionTester.Create(services, hubApi => hubApi.Installations.GetPendingCommands);
    }

    private async Task<AppDeleteCommandDetailModel> PrepareDeletePendingInstallation(IHubActionTester tester, string qualifiedMachineName)
    {
        var hubApp = await tester.HubApp();
        var appVersion = await hubApp.CurrentVersion(ct: default);
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
        await BeginCommand(tester, installCommandDetail.Command);
        var installation = await BeginInstallation(tester, new BeginInstallationRequest(installCommandDetail.Command.ID, true));
        await Installed(tester, new InstallationIDRequest(installation.ID));
        await CommandEnded(tester, installCommandDetail.Command);
        var deleteCommandDetail = await AddDeleteCommand(tester, new InstallationIDRequest(installation.ID));
        return deleteCommandDetail;
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

    private Task<InstallationModel> BeginInstallation(IHubActionTester tester, BeginInstallationRequest requestData)
    {
        var hubApi = tester.Services.GetRequiredService<HubAppApiFactory>().CreateForSuperUser();
        return hubApi.Installations.BeginInstallation.Invoke(requestData);
    }

    private Task<AppCommandModel> BeginCommand(IHubActionTester tester, AppCommandModel command)
    {
        var hubApi = tester.Services.GetRequiredService<HubAppApiFactory>().CreateForSuperUser();
        return hubApi.Installations.BeginCommand.Invoke(new(commandID: command.ID));
    }

    private Task CommandEnded(IHubActionTester tester, AppCommandModel command)
    {
        var hubApi = tester.Services.GetRequiredService<HubAppApiFactory>().CreateForSuperUser();
        return hubApi.Installations.CommandEnded.Invoke(new(commandID: command.ID));
    }

    private Task Installed(IHubActionTester tester, InstallationIDRequest requestData)
    {
        var hubApi = tester.Services.GetRequiredService<HubAppApiFactory>().CreateForSuperUser();
        return hubApi.Installations.Installed.Invoke(requestData);
    }

    private Task<AppDeleteCommandDetailModel> AddDeleteCommand(IHubActionTester tester, InstallationIDRequest requestData)
    {
        var hubApi = tester.Services.GetRequiredService<HubAppApiFactory>().CreateForSuperUser();
        return hubApi.Installations.AddDeleteCommand.Invoke(requestData);
    }

    private Task<AppDeleteCommandDetailModel> GetDeleteCommandDetail(IHubActionTester tester, AppCommandIDRequest requestData)
    {
        var hubApi = tester.Services.GetRequiredService<HubAppApiFactory>().CreateForSuperUser();
        return hubApi.Installations.GetDeleteCommandDetail.Invoke(requestData);
    }
}
