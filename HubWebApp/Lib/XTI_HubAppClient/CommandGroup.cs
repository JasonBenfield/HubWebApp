// Generated Code
namespace XTI_HubAppClient;
public sealed partial class CommandGroup : AppClientGroup
{
    public CommandGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "Command")
    {
        Actions = new CommandGroupActions(BeginCommand: CreatePostAction<AppCommandIDRequest, AppCommandModel>("BeginCommand"), BeginCommandStep: CreatePostAction<BeginAppCommandStepRequest, AppCommandStepModel>("BeginCommandStep"), BeginInstallation: CreatePostAction<BeginInstallationRequest, InstallationModel>("BeginInstallation"), CommandEnded: CreatePostAction<AppCommandIDRequest, EmptyActionResult>("CommandEnded"), CommandStepEnded: CreatePostAction<AppCommandStepEndedRequest, EmptyRequest>("CommandStepEnded"), GetCommand: CreatePostAction<AppCommandIDRequest, AppCommandModel>("GetCommand"), GetDeleteCommandDetail: CreatePostAction<AppCommandIDRequest, AppDeleteCommandDetailModel>("GetDeleteCommandDetail"), GetInstallationCommandDetail: CreatePostAction<AppCommandIDRequest, AppInstallCommandDetailModel>("GetInstallationCommandDetail"), Index: CreateGetAction<AppCommandIDRequest>("Index"));
        Configure();
    }

    partial void Configure();
    public CommandGroupActions Actions { get; }

    public Task<AppCommandModel> BeginCommand(string modifier, AppCommandIDRequest requestData, CancellationToken ct = default) => Actions.BeginCommand.Post(modifier, requestData, ct);
    public Task<AppCommandStepModel> BeginCommandStep(string modifier, BeginAppCommandStepRequest requestData, CancellationToken ct = default) => Actions.BeginCommandStep.Post(modifier, requestData, ct);
    public Task<InstallationModel> BeginInstallation(string modifier, BeginInstallationRequest requestData, CancellationToken ct = default) => Actions.BeginInstallation.Post(modifier, requestData, ct);
    public Task<EmptyActionResult> CommandEnded(string modifier, AppCommandIDRequest requestData, CancellationToken ct = default) => Actions.CommandEnded.Post(modifier, requestData, ct);
    public Task<EmptyRequest> CommandStepEnded(string modifier, AppCommandStepEndedRequest requestData, CancellationToken ct = default) => Actions.CommandStepEnded.Post(modifier, requestData, ct);
    public Task<AppCommandModel> GetCommand(string modifier, AppCommandIDRequest requestData, CancellationToken ct = default) => Actions.GetCommand.Post(modifier, requestData, ct);
    public Task<AppDeleteCommandDetailModel> GetDeleteCommandDetail(string modifier, AppCommandIDRequest requestData, CancellationToken ct = default) => Actions.GetDeleteCommandDetail.Post(modifier, requestData, ct);
    public Task<AppInstallCommandDetailModel> GetInstallationCommandDetail(string modifier, AppCommandIDRequest requestData, CancellationToken ct = default) => Actions.GetInstallationCommandDetail.Post(modifier, requestData, ct);
    public sealed record CommandGroupActions(AppClientPostAction<AppCommandIDRequest, AppCommandModel> BeginCommand, AppClientPostAction<BeginAppCommandStepRequest, AppCommandStepModel> BeginCommandStep, AppClientPostAction<BeginInstallationRequest, InstallationModel> BeginInstallation, AppClientPostAction<AppCommandIDRequest, EmptyActionResult> CommandEnded, AppClientPostAction<AppCommandStepEndedRequest, EmptyRequest> CommandStepEnded, AppClientPostAction<AppCommandIDRequest, AppCommandModel> GetCommand, AppClientPostAction<AppCommandIDRequest, AppDeleteCommandDetailModel> GetDeleteCommandDetail, AppClientPostAction<AppCommandIDRequest, AppInstallCommandDetailModel> GetInstallationCommandDetail, AppClientGetAction<AppCommandIDRequest> Index);
}