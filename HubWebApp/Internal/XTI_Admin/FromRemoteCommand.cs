using XTI_Hub.Abstractions;

namespace XTI_Admin;

internal sealed class FromRemoteCommand : ICommand
{
    private readonly CommandFactory commandFactory;
    private readonly AdminOptions options;
    private readonly StoredObjectFactory storedObjFactory;

    public FromRemoteCommand(CommandFactory commandFactory, AdminOptions options, StoredObjectFactory storedObjFactory)
    {
        this.commandFactory = commandFactory;
        this.options = options;
        this.storedObjFactory = storedObjFactory;
    }

    public async Task Execute()
    {
        if (string.IsNullOrWhiteSpace(options.RemoteOptionsKey))
        {
            throw new Exception("Remote Options Key is required");
        }
        var storageName = new RemoteOptionsStorageName().Value;
        var remoteOptions = await storedObjFactory.CreateStoredObject(storageName)
            .Value<AdminOptions>(options.RemoteOptionsKey);
        options.Load(remoteOptions);
        var command = commandFactory.CreateCommand(options);
        await command.Execute();
    }
}