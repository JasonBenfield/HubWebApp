using System;
using System.Threading.Tasks;
using XTI_App.Abstractions;
using XTI_Git.Abstractions;
using XTI_HubAppApi;
using XTI_HubAppApi.AppRegistration;
using XTI_VersionToolApi;

namespace XTI_Version
{
    public sealed class NewVersionCommand : VersionCommand
    {
        private readonly HubAppApi hubApi;
        private readonly GitFactory gitFactory;

        public NewVersionCommand(HubAppApi hubApi, GitFactory gitFactory)
        {
            this.hubApi = hubApi;
            this.gitFactory = gitFactory;
        }

        public async Task Execute(VersionToolOptions options)
        {
            if (string.IsNullOrWhiteSpace(options.AppName)) { throw new ArgumentException("App Name is required"); }
            if (string.IsNullOrWhiteSpace(options.AppType)) { throw new ArgumentException("App Type is required"); }
            if (string.IsNullOrWhiteSpace(options.RepoOwner)) { throw new ArgumentException("Repo Owner is required"); }
            if (string.IsNullOrWhiteSpace(options.RepoName)) { throw new ArgumentException("Repo Name is required"); }
            var gitRepo = await gitFactory.CreateGitRepo();
            var currentBranchName = gitRepo.CurrentBranchName();
            var gitHubRepo = await gitFactory.CreateGitHubRepo(options.RepoOwner, options.RepoName);
            var defaultBranchName = await gitHubRepo.DefaultBranchName();
            if (!currentBranchName.Equals(defaultBranchName, StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException($"Current branch '{currentBranchName}' is not the default branch '{defaultBranchName}'");
            }
            var versionType = AppVersionType.Values.Value(options.VersionType);
            if (versionType == null)
            {
                throw new ArgumentException($"Version type '{options.VersionType}' is not valid");
            }
            var newVersionResult = await hubApi.AppRegistration.NewVersion.Execute(new NewVersionRequest
            {
                AppKey = options.AppKey(),
                VersionType = versionType
            });
            var version = newVersionResult.Data;
            var gitVersion = new XtiGitVersion(version.VersionType.DisplayText, version.VersionKey);
            await gitHubRepo.CreateNewVersion(gitVersion);
            var newVersionBranchName = gitVersion.BranchName();
            gitRepo.CheckoutBranch(newVersionBranchName.Value);
        }
    }
}
