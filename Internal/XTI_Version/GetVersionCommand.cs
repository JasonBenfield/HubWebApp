using System;
using System.Threading.Tasks;
using XTI_App.Abstractions;
using XTI_Git.Abstractions;
using XTI_Hub;
using XTI_VersionToolApi;

namespace XTI_Version
{
    public sealed class GetVersionCommand : VersionCommand
    {
        private readonly AppFactory appFactory;
        private readonly VersionGitFactory gitFactory;

        public GetVersionCommand(AppFactory appFactory, VersionGitFactory gitFactory)
        {
            this.appFactory = appFactory;
            this.gitFactory = gitFactory;
        }

        public async Task Execute(VersionToolOptions options)
        {
            AppVersion version;
            var gitRepo = gitFactory.CreateGitRepo();
            var currentBranchName = gitRepo.CurrentBranchName();
            var app = await appFactory.Apps.App(options.AppKey());
            var xtiBranchName = XtiBranchName.Parse(currentBranchName);
            if (xtiBranchName is XtiIssueBranchName issueBranchName && !string.IsNullOrWhiteSpace(options.RepoOwner))
            {
                var gitHubRepo = gitFactory.CreateGitHubRepo();
                var issue = await gitHubRepo.Issue(issueBranchName.IssueNumber);
                var milestoneName = XtiMilestoneName.Parse(issue.Milestone.Title);
                var versionKey = AppVersionKey.Parse(milestoneName.Version.Key);
                version = await app.Version(versionKey);
            }
            else if (xtiBranchName is XtiVersionBranchName versionBranchName)
            {
                var versionKey = AppVersionKey.Parse(versionBranchName.Version.Key);
                version = await app.Version(versionKey);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(options.AppName)) { throw new ArgumentException("App Name is required"); }
                if (string.IsNullOrWhiteSpace(options.AppType)) { throw new ArgumentException("App Type is required"); }
                version = await app.CurrentVersion();
            }
            var output = new VersionOutput();
            output.Output(version.ToModel(), options.OutputPath);
        }
    }
}
