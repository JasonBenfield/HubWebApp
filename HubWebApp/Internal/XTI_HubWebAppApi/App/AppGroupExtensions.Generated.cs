using XTI_HubWebAppApiActions.App;
using XTI_HubWebAppApiActions.AppInquiry;

// Generated Code
namespace XTI_HubWebAppApi;
internal static partial class AppGroupExtensions
{
    internal static void AddAppServices(this IServiceCollection services)
    {
        services.AddScoped<AddInstallCommandAction>();
        services.AddScoped<AddInstallCommandValidation>();
        services.AddScoped<ConfigureInstallAction>();
        services.AddScoped<DeleteInstallConfigurationAction>();
        services.AddScoped<GetAppAction>();
        services.AddScoped<GetDefaultAppOptionsAction>();
        services.AddScoped<GetDefaultModifierAction>();
        services.AddScoped<GetDefaultOptionsAction>();
        services.AddScoped<GetInstallConfigurationsAction>();
        services.AddScoped<GetModifierCategoriesAction>();
        services.AddScoped<GetMostRecentErrorEventsAction>();
        services.AddScoped<GetMostRecentRequestsAction>();
        services.AddScoped<GetResourceGroupsAction>();
        services.AddScoped<GetRolesAction>();
        services.AddScoped<IndexAction>();
        services.AddScoped<UpdateVersionsFromPublishedAction>();
    }
}