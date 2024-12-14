using XTI_HubWebAppApiActions;

namespace XTI_HubWebAppApi;

static partial class HubApiExtensions
{
    static partial void AddMoreServices(this IServiceCollection services)
    {
        services.AddScoped<UnverifiedUser>();
    }
}
