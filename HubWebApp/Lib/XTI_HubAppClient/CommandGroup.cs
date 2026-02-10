// Generated Code
namespace XTI_HubAppClient;
public sealed partial class CommandGroup : AppClientGroup
{
    public CommandGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "Command")
    {
        Actions = new CommandGroupActions(Index: CreateGetAction<AppCommandIDRequest>("Index"));
        Configure();
    }

    partial void Configure();
    public CommandGroupActions Actions { get; }

    public sealed record CommandGroupActions(AppClientGetAction<AppCommandIDRequest> Index);
}