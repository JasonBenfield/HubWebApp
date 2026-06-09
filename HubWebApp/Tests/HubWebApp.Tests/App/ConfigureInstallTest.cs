namespace HubWebApp.Tests;

public sealed class ConfigureInstallTest
{
    [Test]
    public async Task ShouldConfigureInstall()
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
        var installConfiguration = await ConfigureInstall
        (
            sp: sp, 
            configurationName: "Default", 
            template: installTemplate, 
            installSequence: 10
        );
        Assert.That(installConfiguration.ConfigurationName, Is.EqualTo("Default"));
        Assert.That(installConfiguration.Template, Is.EqualTo(installTemplate));
        Assert.That(installConfiguration.InstallSequence, Is.EqualTo(10));
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

    private async Task<InstallConfigurationModel> ConfigureInstall(IServiceProvider sp, string configurationName, InstallConfigurationTemplateModel template, int installSequence)
    {
        var tester = HubActionTester.Create(sp, api => api.App.ConfigureInstall);
        var hubAppModifier = await tester.HubAppModifier();
        var configuration = await tester.Execute
        (
            new ConfigureAppInstallRequest
            (
                configurationName: configurationName, 
                templateID: template.ID, 
                installSequence: installSequence
            ),
            hubAppModifier.ModKey
        );
        return configuration;
    }
}
