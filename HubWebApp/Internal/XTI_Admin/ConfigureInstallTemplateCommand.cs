using XTI_Hub;
using XTI_Hub.Abstractions;

namespace XTI_Admin;

internal sealed class ConfigureInstallTemplateCommand : ICommand
{
    private readonly IHubService hubAdmin;
    private readonly AdminOptions adminOptions;

    public ConfigureInstallTemplateCommand(IHubService hubAdmin, AdminOptions adminOptions)
    {
        this.hubAdmin = hubAdmin;
        this.adminOptions = adminOptions;
    }

    public Task Execute(CancellationToken ct) =>
        hubAdmin.ConfigureInstallTemplate
        (
            new ConfigureInstallTemplateRequest
            (
                templateName: adminOptions.InstallTemplateName,
                destinationMachineName: adminOptions.DestinationMachine,
                domain: adminOptions.Domain,
                siteName: adminOptions.SiteName
            ),
            ct
        );
}
