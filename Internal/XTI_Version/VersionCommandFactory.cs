using System;
using XTI_HubAppApi;

namespace XTI_Version
{
    public sealed class VersionCommandFactory
    {
        private readonly HubAppApi hubApi;
        private readonly GitFactory gitFactory;

        public VersionCommandFactory(HubAppApi hubApi, GitFactory gitFactory)
        {
            this.hubApi = hubApi;
            this.gitFactory = gitFactory;
        }

        public VersionCommand Create(VersionCommandName commandName)
        {
            VersionCommand command;
            if (commandName.Equals(VersionCommandName.NewVersion))
            {
                command = new NewVersionCommand(hubApi, gitFactory);
            }
            else if (commandName.Equals(VersionCommandName.NewIssue))
            {
                command = new NewIssueCommand(gitFactory);
            }
            else if (commandName.Equals(VersionCommandName.Issues))
            {
                command = new IssuesCommand(gitFactory);
            }
            else if (commandName.Equals(VersionCommandName.StartIssue))
            {
                command = new StartIssueCommand(gitFactory);
            }
            else if (commandName.Equals(VersionCommandName.CompleteIssue))
            {
                command = new CompleteIssueCommand(gitFactory);
            }
            else if (commandName.Equals(VersionCommandName.GetCurrentVersion))
            {
                command = new GetCurrentVersionCommand(hubApi);
            }
            else if (commandName.Equals(VersionCommandName.GetVersion))
            {
                command = new GetVersionCommand(hubApi, gitFactory);
            }
            else if (commandName.Equals(VersionCommandName.BeginPublish))
            {
                command = new BeginPublishCommand(hubApi, gitFactory);
            }
            else if (commandName.Equals(VersionCommandName.CompleteVersion))
            {
                command = new CompleteVersionCommand(hubApi, gitFactory);
            }
            else
            {
                throw new NotSupportedException($"Command '{commandName.Value}' is not supported");
            }
            return command;
        }

    }
}
