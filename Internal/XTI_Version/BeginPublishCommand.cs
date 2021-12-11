using System;
using System.Threading.Tasks;
using XTI_App.Abstractions;
using XTI_Git.Abstractions;
using XTI_Hub;
using XTI_VersionToolApi;

namespace XTI_Version
{
    public sealed class BeginPublishCommand : VersionCommand
    {
        private readonly AppFactory appFactory;
        private readonly GitFactory gitFactory;

        public BeginPublishCommand(AppFactory appFactory, GitFactory gitFactory)
        {
            this.appFactory = appFactory;
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
            var versionKey = AppVersionKey.Parse(versionBranchName.Version.Key);
            var app = await appFactory.Apps.App(options.AppKey());
            var version = await app.Version(versionKey);
            await version.Publishing();
            var output = new VersionOutput();
            output.Output(version.ToModel(), options.OutputPath);
        }
    }
}
