using XTI_AuthenticatorWebAppApi.Home;

// Generated Code
#nullable enable
namespace XTI_AuthenticatorWebAppApi.Home;
public sealed partial class HomeGroupBuilder
{
    private readonly AppApiGroup source;
    internal HomeGroupBuilder(AppApiGroup source)
    {
        this.source = source;
        Index = source.AddAction<EmptyRequest, WebViewResult>("Index").WithExecution<IndexAction>();
        Configure();
    }

    partial void Configure();
    public AppApiActionBuilder<EmptyRequest, WebViewResult> Index { get; }

    public HomeGroup Build() => new HomeGroup(source, this);
}