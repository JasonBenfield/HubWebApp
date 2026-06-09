namespace HubWebApp.Tests;

public sealed class DeleteInstallConfigurationTest
{
    [Test]
    public async Task ShouldDeleteInstallConfiguration()
    {
        var sp = await Setup();
        var installTemplate = await ConfigureInstallTemplate
        (
            sp,
            new ConfigureInstallTemplateRequest
            (
                templateName: "Default",
                destinationMachineName: "server.example.com",
                domain: "apps.example.com",
                siteName: "Default"
            )
        );
        var modifier = await sp.HubAppModifier();
        var installConfiguration = await ConfigureInstall
        (
            sp: sp,
            configurationName: "Default",
            template: installTemplate,
            installSequence: 10,
            modKey: modifier.ModKey
        );
        await DeleteInstallConfiguration(sp, installConfiguration, modifier.ModKey);
        var installConfigurations = await GetInstallConfigurations(sp, modifier.ModKey);
        Assert.That(installConfigurations.Length, Is.EqualTo(0));
    }

    private Task<IServiceProvider> Setup()
    {
        var host = new HubTestHost();
        return host.Setup();
    }

    private Task<InstallConfigurationTemplateModel> ConfigureInstallTemplate(IServiceProvider sp, ConfigureInstallTemplateRequest requestData)
    {
        var tester = HubActionTester.Create(sp, api => api.InstallTemplates.ConfigureInstallTemplate);
        return tester.Execute(requestData);
    }

    private Task<InstallConfigurationModel> ConfigureInstall(IServiceProvider sp, string configurationName, InstallConfigurationTemplateModel template, int installSequence, ModifierKey modKey)
    {
        var tester = HubActionTester.Create(sp, api => api.App.ConfigureInstall);
        return tester.Execute
        (
            new ConfigureAppInstallRequest
            (
                configurationName: configurationName,
                templateID: template.ID,
                installSequence: installSequence
            ),
            modKey
        );
    }

    private Task DeleteInstallConfiguration(IServiceProvider sp, InstallConfigurationModel installConfiguration, ModifierKey modKey)
    {
        var tester = HubActionTester.Create(sp, api => api.App.DeleteInstallConfiguration);
        return tester.Execute
        (
            new InstallConfigurationIDRequest(configurationID: installConfiguration.ID),
            modKey
        );
    }

    private Task<InstallConfigurationModel[]> GetInstallConfigurations(IServiceProvider sp, ModifierKey modKey)
    {
        var tester = HubActionTester.Create(sp, api => api.App.GetInstallConfigurations);
        return tester.Execute(new(), modKey);
    }
}
