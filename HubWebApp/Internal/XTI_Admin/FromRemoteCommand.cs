using XTI_Core;
using XTI_Hub;

namespace XTI_Admin;

internal sealed class FromRemoteCommand : ICommand
{
    private readonly CommandFactory commandFactory;
    private readonly AdminOptions options;
    private readonly IHubAdministration hubAdmin;

    public FromRemoteCommand(CommandFactory commandFactory, AdminOptions options, IHubAdministration hubAdmin)
    {
        this.commandFactory = commandFactory;
        this.options = options;
        this.hubAdmin = hubAdmin;
    }

    public async Task Execute(CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(options.RemoteOptionsKey))
        {
            throw new Exception("Remote Options Key is required");
        }
        var storageName = new RemoteOptionsStorageName().Value;
        var serializedRemoteOptions = await hubAdmin.StoredObject(storageName, options.RemoteOptionsKey, ct);
        var remoteOptions = string.IsNullOrWhiteSpace(serializedRemoteOptions) ?
            new AdminOptions() :
            XtiSerializer.Deserialize<AdminOptions>(serializedRemoteOptions);
        options.Load(remoteOptions);
        var command = commandFactory.CreateCommand(options);
        await command.Execute(ct);
    }
}