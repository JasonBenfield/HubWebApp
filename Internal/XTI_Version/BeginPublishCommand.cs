using System;
using System.Threading.Tasks;
using XTI_App.Abstractions;
using XTI_Git.Abstractions;
using XTI_HubAppApi;
using XTI_HubAppApi.AppRegistration;
using XTI_VersionToolApi;

namespace XTI_Version
{
    public sealed class BeginPublishCommand : VersionCommand
    {
        private readonly HubAppApi hubApi;
        private readonly GitFactory gitFactory;

        public BeginPublishCommand(HubAppApi hubApi, GitFactory gitFactory)
        {
            this.hubApi = hubApi;
            this.gitFactory = gitFactory;
        }

        public async Task Execute(VersionToolOptions options)
        {
            if (string.IsNullOrWhiteSpace(options.AppName)) { throw new ArgumentException("App Name is required"); }
            if (string.IsNullOrWhiteSpace(options.AppType)) { throw new ArgumentException("App Type is required"); }
            var gitRepo = await gitFactory.CreateGitRepo();
            var branchName = gitRepo.CurrentBranchName();
            var xtiBranchName = XtiBranchName.Parse(branchName);
            if (xtiBranchName is not XtiVersionBranchName versionBranchName)
            {
                throw new ArgumentException($"Branch '{branchName}' is not a version branch");
            }
            var version = await hubApi.AppRegistration.BeginPublish.Invoke(new GetVersionRequest
            {
                AppKey = options.AppKey(),
                VersionKey = AppVersionKey.Parse(versionBranchName.Version.Key)
            });
            var output = new VersionOutput();
            output.Output(version, options.OutputPath);
        }
    }
}
