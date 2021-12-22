using XTI_Core;
using XTI_Hub;

namespace XTI_Version;

public sealed class VersionCommandFactory
{
    private readonly AppFactory appFactory;
    private readonly GitFactory gitFactory;
    private readonly IClock clock;

    public VersionCommandFactory(AppFactory appFactory, GitFactory gitFactory, IClock clock)
    {
        this.appFactory = appFactory;
        this.gitFactory = gitFactory;
        this.clock = clock;
    }

    public VersionCommand Create(VersionCommandName commandName)
    {
        VersionCommand command;
        if (commandName.Equals(VersionCommandName.NewVersion))
        {
            command = new NewVersionCommand(appFactory, gitFactory, clock);
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
            command = new GetCurrentVersionCommand(appFactory);
        }
        else if (commandName.Equals(VersionCommandName.GetVersion))
        {
            command = new GetVersionCommand(appFactory, gitFactory);
        }
        else if (commandName.Equals(VersionCommandName.BeginPublish))
        {
            command = new BeginPublishCommand(appFactory, gitFactory);
        }
        else if (commandName.Equals(VersionCommandName.CompleteVersion))
        {
            command = new CompleteVersionCommand(appFactory, gitFactory);
        }
        else
        {
            throw new NotSupportedException($"Command '{commandName.Value}' is not supported");
        }
        return command;
    }
}