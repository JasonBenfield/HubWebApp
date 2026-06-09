using XTI_HubWebAppApiActions.InstallTemplates;

// Generated Code
#nullable enable
namespace XTI_HubWebAppApi.InstallTemplates;
public sealed partial class InstallTemplatesGroup : AppApiGroupWrapper
{
    internal InstallTemplatesGroup(AppApiGroup source, InstallTemplatesGroupBuilder builder) : base(source)
    {
        ConfigureInstallTemplate = builder.ConfigureInstallTemplate.Build();
        GetInstallTemplates = builder.GetInstallTemplates.Build();
        Index = builder.Index.Build();
        Configure();
    }

    partial void Configure();
    public AppApiAction<ConfigureInstallTemplateRequest, InstallConfigurationTemplateModel> ConfigureInstallTemplate { get; }
    public AppApiAction<EmptyRequest, InstallConfigurationTemplateModel[]> GetInstallTemplates { get; }
    public AppApiAction<EmptyRequest, WebViewResult> Index { get; }
}