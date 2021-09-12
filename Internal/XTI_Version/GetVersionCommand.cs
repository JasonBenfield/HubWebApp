using System;
using System.Threading.Tasks;
using XTI_Hub;
using XTI_App.Abstractions;
using XTI_Git.Abstractions;
using XTI_VersionToolApi;
using XTI_HubAppApi;
using XTI_HubAppApi.AppRegistration;

namespace XTI_Version
{
    public sealed class GetVersionCommand : VersionCommand
    {
        private readonly HubAppApi hubApi;
        private readonly GitFactory gitFactory;

        public GetVersionCommand(HubAppApi hubApi, GitFactory gitFactory)
        {
            this.hubApi = hubApi;
            this.gitFactory = gitFactory;
        }

        public async Task Execute(VersionToolOptions options)
        {
            AppVersionModel version;
            var gitRepo = await gitFactory.CreateGitRepo();
            var currentBranchName = gitRepo.CurrentBranchName();
            var xtiBranchName = XtiBranchName.Parse(currentBranchName);
            if (xtiBranchName is XtiIssueBranchName issueBranchName && !string.IsNullOrWhiteSpace(options.RepoOwner))
            {
                var gitHubRepo = await gitFactory.CreateGitHubRepo(options.RepoOwner, options.RepoName);
                var issue = await gitHubRepo.Issue(issueBranchName.IssueNumber);
                var milestoneName = XtiMilestoneName.Parse(issue.Milestone.Title);
                var versionKey = AppVersionKey.Parse(milestoneName.Version.Key);
                version = await hubApi.AppRegistration.GetVersion.Invoke(new GetVersionRequest
                {
                    AppKey = options.AppKey(),
                    VersionKey = versionKey
                });
            }
            else if (xtiBranchName is XtiVersionBranchName versionBranchName)
            {
                var versionKey = AppVersionKey.Parse(versionBranchName.Version.Key);
                version = await hubApi.AppRegistration.GetVersion.Invoke(new GetVersionRequest
                {
                    AppKey = options.AppKey(),
                    VersionKey = versionKey
                });
            }
            else
            {
                if (string.IsNullOrWhiteSpace(options.AppName)) { throw new ArgumentException("App Name is required"); }
                if (string.IsNullOrWhiteSpace(options.AppType)) { throw new ArgumentException("App Type is required"); }
                version = await hubApi.AppRegistration.GetVersion.Invoke(new GetVersionRequest
                {
                    AppKey = options.AppKey(),
                    VersionKey = AppVersionKey.Current
                });
            }
            var output = new VersionOutput();
            output.Output(version, options.OutputPath);
        }
    }
}
