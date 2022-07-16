using XTI_HubWebAppApi.VersionInquiry;

namespace XTI_HubWebAppApi;

internal static class VersionInquiryExtensions
{
    public static void AddVersionInquiryGroupServices(this IServiceCollection services)
    {
        services.AddScoped<GetVersionAction>();
    }
}