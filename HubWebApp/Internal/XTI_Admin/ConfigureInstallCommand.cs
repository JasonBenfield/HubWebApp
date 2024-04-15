using XTI_Hub;
using XTI_Hub.Abstractions;

namespace XTI_Admin;

internal sealed class ConfigureInstallCommand : ICommand
{
    private readonly IHubAdministration hubAdmin;
    private readonly AdminOptions adminOptions;
    private readonly GitRepoInfo gitRepoInfo;

    public ConfigureInstallCommand(IHubAdministration hubAdmin, AdminOptions adminOptions, GitRepoInfo gitRepoInfo)
    {
        this.hubAdmin = hubAdmin;
        this.adminOptions = adminOptions;
        this.gitRepoInfo = gitRepoInfo;
    }

    public Task Execute(CancellationToken ct)
    {
        var configName = string.IsNullOrWhiteSpace(adminOptions.InstallConfigurationName) ?
            "Default" :
            adminOptions.InstallConfigurationName;
        var appKey = adminOptions.AppKey();
        return hubAdmin.ConfigureInstall
        (
            new ConfigureInstallRequest
            (
                repoOwner: gitRepoInfo.RepoOwner,
                repoName: gitRepoInfo.RepoName,
                configurationName: configName,
                appKey: appKey,
                templateName: adminOptions.InstallTemplateName,
                installSequence: adminOptions.InstallSequence
            ),
            ct
        );
    }
}
