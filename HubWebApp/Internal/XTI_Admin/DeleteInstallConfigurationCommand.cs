using XTI_Hub;
using XTI_Hub.Abstractions;

namespace XTI_Admin;

internal sealed class DeleteInstallConfigurationCommand : ICommand
{
    private readonly IHubAdministration hubAdmin;
    private readonly AdminOptions adminOptions;
    private readonly GitRepoInfo gitRepoInfo;

    public DeleteInstallConfigurationCommand(IHubAdministration hubAdmin, AdminOptions adminOptions, GitRepoInfo gitRepoInfo)
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
        return hubAdmin.DeleteInstallConfiguration
        (
            new DeleteInstallConfigurationRequest
            (
                repoOwner: gitRepoInfo.RepoOwner,
                repoName: gitRepoInfo.RepoName,
                configurationName: configName,
                appKey: appKey
            ),
            ct
        );
    }
}
