using System;
using System.Threading.Tasks;
using XTI_App.Abstractions;
using XTI_Git.Abstractions;
using XTI_Hub;
using XTI_VersionToolApi;

namespace XTI_Version
{
    public sealed class CompleteVersionCommand : VersionCommand
    {
        private readonly AppFactory appFactory;
        private readonly GitFactory gitFactory;

        public CompleteVersionCommand(AppFactory appFactory, GitFactory gitFactory)
        {
            this.appFactory = appFactory;
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
            var xtiBranchName = XtiBranchName.Parse(currentBranchName);
            if (xtiBranchName is not XtiVersionBranchName versionBranchName)
            {
                throw new ArgumentException($"Branch '{currentBranchName}' is not a version branch");
            }
            var gitHubRepo = await gitFactory.CreateGitHubRepo(options.RepoOwner, options.RepoName);
            gitRepo.CommitChanges($"Version {versionBranchName.Version.Key}");
            await gitHubRepo.CompleteVersion(versionBranchName);
            var defaultBranchName = await gitHubRepo.DefaultBranchName();
            gitRepo.CheckoutBranch(defaultBranchName);
            var versionKey = AppVersionKey.Parse(versionBranchName.Version.Key);
            var app = await appFactory.Apps.App(options.AppKey());
            var version = await app.Version(versionKey);
            await version.Published();
            gitRepo.DeleteBranch(versionBranchName.Value);
        }
    }
}
