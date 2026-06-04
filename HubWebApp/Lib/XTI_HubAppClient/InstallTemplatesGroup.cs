// Generated Code
namespace XTI_HubAppClient;
public sealed partial class InstallTemplatesGroup : AppClientGroup
{
    public InstallTemplatesGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "InstallTemplates")
    {
        Actions = new InstallTemplatesGroupActions(GetInstallTemplates: CreatePostAction<EmptyRequest, InstallConfigurationTemplateModel[]>("GetInstallTemplates"), Index: CreateGetAction<EmptyRequest>("Index"));
        Configure();
    }

    partial void Configure();
    public InstallTemplatesGroupActions Actions { get; }

    public Task<InstallConfigurationTemplateModel[]> GetInstallTemplates(CancellationToken ct = default) => Actions.GetInstallTemplates.Post("", new EmptyRequest(), ct);
    public sealed record InstallTemplatesGroupActions(AppClientPostAction<EmptyRequest, InstallConfigurationTemplateModel[]> GetInstallTemplates, AppClientGetAction<EmptyRequest> Index);
}