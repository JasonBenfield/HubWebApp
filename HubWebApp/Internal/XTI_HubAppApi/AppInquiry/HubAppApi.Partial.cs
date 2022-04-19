using XTI_Hub;
using XTI_HubAppApi.AppInquiry;

namespace XTI_HubAppApi;

partial class HubAppApi
{
    private AppInquiryGroup? app;

    public AppInquiryGroup App { get => app ?? throw new ArgumentNullException(nameof(app)); }

    partial void createAppInquiry(IServiceProvider services)
    {
        app = new AppInquiryGroup
        (
            source.AddGroup
            (
                nameof(App),
                HubInfo.ModCategories.Apps,
                Access.WithAllowed(HubInfo.Roles.ViewApp)
            ),
            services
        );
    }
}