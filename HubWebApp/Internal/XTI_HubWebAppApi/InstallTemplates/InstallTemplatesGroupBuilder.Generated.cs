using XTI_HubWebAppApiActions.InstallTemplates;

// Generated Code
#nullable enable
namespace XTI_HubWebAppApi.InstallTemplates;
public sealed partial class InstallTemplatesGroupBuilder
{
    private readonly AppApiGroup source;
    internal InstallTemplatesGroupBuilder(AppApiGroup source)
    {
        this.source = source;
        ConfigureInstallTemplate = source.AddAction<ConfigureInstallTemplateRequest, InstallConfigurationTemplateModel>("ConfigureInstallTemplate").WithExecution<ConfigureInstallTemplateAction>().WithValidation<ConfigureInstallTemplateValidation>();
        GetInstallTemplates = source.AddAction<EmptyRequest, InstallConfigurationTemplateModel[]>("GetInstallTemplates").WithExecution<GetInstallTemplatesAction>();
        Index = source.AddAction<EmptyRequest, WebViewResult>("Index").WithExecution<IndexAction>();
        Configure();
    }

    partial void Configure();
    public AppApiActionBuilder<ConfigureInstallTemplateRequest, InstallConfigurationTemplateModel> ConfigureInstallTemplate { get; }
    public AppApiActionBuilder<EmptyRequest, InstallConfigurationTemplateModel[]> GetInstallTemplates { get; }
    public AppApiActionBuilder<EmptyRequest, WebViewResult> Index { get; }

    public InstallTemplatesGroup Build() => new InstallTemplatesGroup(source, this);
}