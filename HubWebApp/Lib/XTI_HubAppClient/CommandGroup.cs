// Generated Code
namespace XTI_HubAppClient;
public sealed partial class CommandGroup : AppClientGroup
{
    public CommandGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "Command")
    {
        Actions = new CommandGroupActions(GetCommand: CreatePostAction<AppCommandIDRequest, AppCommandModel>("GetCommand"), GetDeleteCommandDetail: CreatePostAction<AppCommandIDRequest, AppDeleteCommandDetailModel>("GetDeleteCommandDetail"), GetInstallationCommandDetail: CreatePostAction<AppCommandIDRequest, AppInstallCommandDetailModel>("GetInstallationCommandDetail"), Index: CreateGetAction<AppCommandIDRequest>("Index"));
        Configure();
    }

    partial void Configure();
    public CommandGroupActions Actions { get; }

    public Task<AppCommandModel> GetCommand(string modifier, AppCommandIDRequest requestData, CancellationToken ct = default) => Actions.GetCommand.Post(modifier, requestData, ct);
    public Task<AppDeleteCommandDetailModel> GetDeleteCommandDetail(string modifier, AppCommandIDRequest requestData, CancellationToken ct = default) => Actions.GetDeleteCommandDetail.Post(modifier, requestData, ct);
    public Task<AppInstallCommandDetailModel> GetInstallationCommandDetail(string modifier, AppCommandIDRequest requestData, CancellationToken ct = default) => Actions.GetInstallationCommandDetail.Post(modifier, requestData, ct);
    public sealed record CommandGroupActions(AppClientPostAction<AppCommandIDRequest, AppCommandModel> GetCommand, AppClientPostAction<AppCommandIDRequest, AppDeleteCommandDetailModel> GetDeleteCommandDetail, AppClientPostAction<AppCommandIDRequest, AppInstallCommandDetailModel> GetInstallationCommandDetail, AppClientGetAction<AppCommandIDRequest> Index);
}