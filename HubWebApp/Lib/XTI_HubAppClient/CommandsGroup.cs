// Generated Code
namespace XTI_HubAppClient;
public sealed partial class CommandsGroup : AppClientGroup
{
    public CommandsGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "Commands")
    {
        Actions = new CommandsGroupActions(GetCommandsInProgress: CreatePostAction<EmptyRequest, AppCommandSummaryModel[]>("GetCommandsInProgress"), GetPendingCommands: CreatePostAction<GetPendingCommandsRequest, AppCommandSummaryModel[]>("GetPendingCommands"), Index: CreateGetAction<AppCommandIDRequest>("Index"));
        Configure();
    }

    partial void Configure();
    public CommandsGroupActions Actions { get; }

    public Task<AppCommandSummaryModel[]> GetCommandsInProgress(CancellationToken ct = default) => Actions.GetCommandsInProgress.Post("", new EmptyRequest(), ct);
    public Task<AppCommandSummaryModel[]> GetPendingCommands(GetPendingCommandsRequest requestData, CancellationToken ct = default) => Actions.GetPendingCommands.Post("", requestData, ct);
    public sealed record CommandsGroupActions(AppClientPostAction<EmptyRequest, AppCommandSummaryModel[]> GetCommandsInProgress, AppClientPostAction<GetPendingCommandsRequest, AppCommandSummaryModel[]> GetPendingCommands, AppClientGetAction<AppCommandIDRequest> Index);
}