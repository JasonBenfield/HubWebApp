using XTI_AuthenticatorWebAppApi.Home;

// Generated Code
#nullable enable
namespace XTI_AuthenticatorWebAppApi.Home;
public sealed partial class HomeGroup : AppApiGroupWrapper
{
    internal HomeGroup(AppApiGroup source, HomeGroupBuilder builder) : base(source)
    {
        Index = builder.Index.Build();
        Configure();
    }

    partial void Configure();
    public AppApiAction<EmptyRequest, WebViewResult> Index { get; }
}