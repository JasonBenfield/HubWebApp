using Microsoft.Extensions.DependencyInjection;
using XTI_HubAppApi.VersionInquiry;

namespace XTI_HubAppApi
{
    internal static class VersionInquiryExtensions
    {
        public static void AddVersionInquiryGroupServices(this IServiceCollection services)
        {
            services.AddScoped<GetResourceGroupAction>();
            services.AddScoped<GetVersionAction>();
        }
    }
}
