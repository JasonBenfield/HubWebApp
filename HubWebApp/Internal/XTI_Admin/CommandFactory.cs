using System.Net.Http;
using XTI_App.Secrets;
using XTI_Core;
using XTI_Git;
using XTI_GitHub;
using XTI_Hub;
using XTI_Hub.Abstractions;
using XTI_PermanentLog;
using XTI_Secrets;
using XTI_TempLog;

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
        if (commandName == CommandNames.NotSet) { throw new ArgumentException("Command is required."); }
        if (commandName == CommandNames.PublishAndInstall)
        {
            command = new PublishAndInstallCommand
            (
                scopes.GetRequiredService<SlnFolder>(),
                scopes.GetRequiredService<BuildProcess>(),
                scopes.GetRequiredService<PublishProcess>(),
                scopes.GetRequiredService<InstallProcess>()
            );
        }
        else if (commandName == CommandNames.Publish)
        {
            command = new PublishCommand
            (
                scopes.GetRequiredService<SlnFolder>(),
                scopes.GetRequiredService<BuildProcess>(),
                scopes.GetRequiredService<PublishProcess>()
            );
        }
        else if (commandName == CommandNames.PublishLib)
        {
            command = new PublishLibCommand
            (
                scopes.GetRequiredService<XtiEnvironment>(),
                scopes.GetRequiredService<CurrentVersion>(),
                scopes.GetRequiredService<IXtiGitRepository>(),
                scopes.GetRequiredService<XtiGitHubRepository>(),
                scopes.GetRequiredService<SelectedAppKeys>(),
                scopes.GetRequiredService<VersionKeyFromCurrentBranch>(),
                scopes.GetRequiredService<PublishLibProcess>()
            );
        }
        else if (commandName == CommandNames.Install)
        {
            command = new InstallCommand
            (
                scopes.GetRequiredService<SlnFolder>(),
                scopes.GetRequiredService<InstallProcess>()
            );
        }
        else if (commandName == CommandNames.FromRemote)
        {
            command = new FromRemoteCommand
            (
                this,
                scopes.GetRequiredService<AdminOptions>(),
                scopes.GetRequiredService<IHubAdministration>()
            );
        }
        else if (commandName == CommandNames.Build)
        {
            command = new BuildCommand(scopes.GetRequiredService<BuildProcess>());
        }
        else if (commandName == CommandNames.Setup)
        {
            command = new SetupCommand
            (
                scopes.GetRequiredService<AdminOptions>(),
                scopes.GetRequiredService<XtiEnvironment>(),
                scopes.GetRequiredService<PublishedAssetsFactory>(),
                scopes.GetRequiredService<IHubAdministration>(),
                scopes.GetRequiredService<SelectedAppKeys>(),
                scopes.GetRequiredService<AppVersionNameAccessor>(),
                scopes.GetRequiredService<CurrentVersion>(),
                scopes.GetRequiredService<PublishSetupProcess>()
            );
        }
        else if (commandName == CommandNames.NewVersion)
        {
            command = new NewVersionCommand
            (
                scopes.GetRequiredService<IXtiGitRepository>(),
                scopes.GetRequiredService<XtiGitHubRepository>(),
                scopes.GetRequiredService<AdminOptions>(),
                scopes.GetRequiredService<IHubAdministration>(),
                scopes.GetRequiredService<AppVersionNameAccessor>()
            );
        }
        else if (commandName == CommandNames.NewIssue)
        {
            command = new NewIssueCommand
            (
                scopes.GetRequiredService<AdminOptions>(),
                scopes.GetRequiredService<IXtiGitRepository>(),
                scopes.GetRequiredService<XtiGitHubRepository>()
            );
        }
        else if (commandName == CommandNames.StartIssue)
        {
            command = new StartIssueCommand
            (
                scopes.GetRequiredService<AdminOptions>(),
                scopes.GetRequiredService<IXtiGitRepository>(),
                scopes.GetRequiredService<XtiGitHubRepository>()
            );
        }
        else if (commandName == CommandNames.CompleteIssue)
        {
            command = new CompleteIssueCommand
            (
                scopes.GetRequiredService<GitRepoInfo>(),
                scopes.GetRequiredService<IXtiGitRepository>(),
                scopes.GetRequiredService<XtiGitHubRepository>()
            );
        }
        else if (commandName == CommandNames.AddInstallationUser)
        {
            command = new AddInstallationUserCommand
            (
                scopes.GetRequiredService<IHubAdministration>(),
                scopes.GetRequiredService<InstallationUserCredentials>()
            );
        }
        else if (commandName == CommandNames.AddSystemUser)
        {
            command = new AddSystemUserCommand
            (
                scopes.GetRequiredService<SelectedAppKeys>(),
                scopes.GetRequiredService<IHubAdministration>(),
                scopes.GetRequiredService<ISecretCredentialsFactory>(),
                scopes.GetRequiredService<AppVersionNameAccessor>()
            );
        }
        else if (commandName == CommandNames.AddAdminUser)
        {
            command = new AddAdminUserCommand
            (
                scopes.GetRequiredService<IHubAdministration>(),
                scopes.GetRequiredService<AdminOptions>(),
                scopes.GetRequiredService<SelectedAppKeys>(),
                scopes.GetRequiredService<ISecretCredentialsFactory>()
            );
        }
        else if (commandName == CommandNames.ShowCredentials)
        {
            command = new ShowCredentialsCommand
            (
                scopes.GetRequiredService<AdminOptions>(),
                scopes.GetRequiredService<ISecretCredentialsFactory>()
            );
        }
        else if (commandName == CommandNames.StoreCredentials)
        {
            command = new StoreCredentialsCommand
            (
                scopes.GetRequiredService<AdminOptions>(),
                scopes.GetRequiredService<ISecretCredentialsFactory>(),
                scopes.GetRequiredService<RemoteCommandService>()
            );
        }
        else if (commandName == CommandNames.DecryptTempLog)
        {
            command = new DecryptTempLogCommand
            (
                scopes.GetRequiredService<IClock>(),
                scopes.GetRequiredService<ITempLogsV1>(),
                scopes.GetRequiredService<TempLog>(),
                scopes.GetRequiredService<XtiFolder>()
            );
        }
        else if (commandName == CommandNames.UploadTempLog)
        {
            command = new UploadTempLogCommand
            (
                scopes.GetRequiredService<TempToPermanentLog>(),
                scopes.GetRequiredService<TempToPermanentLogV1>(),
                scopes.GetRequiredService<AdminOptions>(),
                scopes.GetRequiredService<RemoteCommandService>()
            );
        }
        else if (commandName == CommandNames.RestartApp)
        {
            command = new RestartAppCommand
            (
                scopes.GetRequiredService<AdminOptions>(),
                scopes.GetRequiredService<XtiEnvironment>(),
                scopes.GetRequiredService<XtiFolder>(),
                scopes.GetRequiredService<RemoteCommandService>()
            );
        }
        else if (commandName == CommandNames.ConfigureInstallTemplate)
        {
            command = new ConfigureInstallTemplateCommand
            (
                scopes.GetRequiredService<IHubAdministration>(),
                scopes.GetRequiredService<AdminOptions>()
            );
        }
        else if (commandName == CommandNames.ConfigureInstall)
        {
            command = new ConfigureInstallCommand
            (
                scopes.GetRequiredService<IHubAdministration>(),
                scopes.GetRequiredService<AdminOptions>(),
                scopes.GetRequiredService<GitRepoInfo>()
            );
        }
        else if (commandName == CommandNames.DeleteInstallConfiguration)
        {
            command = new DeleteInstallConfigurationCommand
            (
                scopes.GetRequiredService<IHubAdministration>(),
                scopes.GetRequiredService<AdminOptions>(),
                scopes.GetRequiredService<GitRepoInfo>()
            );
        }
        else
        {
            throw new NotSupportedException($"Command '{options.Command}' is not supported");
        }
        return command;
    }
}