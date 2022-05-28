namespace XTI_Admin;

public sealed class CommandFactory
{
    private readonly Scopes scopes;

    public CommandFactory(Scopes scopes)
    {
        this.scopes = scopes;
    }

    public ICommand CreateCommand(AdminOptions options)
    {
        ICommand command;
        Console.WriteLine($"Command: '{options.Command}'");
        var commandName = options.Command;
        if(commandName == CommandNames.NotSet) { throw new ArgumentException("Command is required."); }
        if (commandName == CommandNames.PublishAndInstall)
        {
            command = new PublishAndInstallCommand(scopes);
        }
        else if (commandName == CommandNames.Publish)
        {
            command = new PublishCommand(scopes);
        }
        else if (commandName == CommandNames.PublishLib)
        {
            command = new PublishLibCommand(scopes);
        }
        else if (commandName == CommandNames.Install)
        {
            command = new InstallCommand(scopes);
        }
        else if (commandName == CommandNames.LocalInstall)
        {
            command = new LocalInstallCommand(scopes);
        }
        else if (commandName == CommandNames.Build)
        {
            command = new BuildCommand(scopes);
        }
        else if (commandName == CommandNames.Setup)
        {
            command = new SetupCommand(scopes);
        }
        else if (commandName == CommandNames.NewVersion)
        {
            command = new NewVersionCommand(scopes);
        }
        else if (commandName == CommandNames.NewIssue)
        {
            command = new NewIssueCommand(scopes);
        }
        else if (commandName == CommandNames.StartIssue)
        {
            command = new StartIssueCommand(scopes);
        }
        else if (commandName == CommandNames.CompleteIssue)
        {
            command = new CompleteIssueCommand(scopes);
        }
        else if (commandName == CommandNames.AddInstallationUser)
        {
            command = new AddInstallationUserCommand(scopes);
        }
        else if(commandName == CommandNames.AddSystemUser)
        {
            command = new AddSystemUserCommand(scopes);
        }
        else if (commandName == CommandNames.AddAdminUser)
        {
            command = new AddAdminUserCommand(scopes);
        }
        else if(commandName == CommandNames.ShowCredentials)
        {
            command = new ShowCredentialsCommand(scopes);
        }
        else if(commandName == CommandNames.StoreCredentials)
        {
            command = new StoreCredentialsCommand(scopes);
        }
        else if(commandName == CommandNames.DecryptTempLog)
        {
            command = new DecryptTempLogCommand(scopes);
        }
        else if (commandName == CommandNames.UploadTempLog)
        {
            command = new UploadTempLogCommand(scopes);
        }
        else
        {
            throw new NotSupportedException($"Command '{options.Command}' is not supported");
        }
        return command;
    }
}