using XTI_HubWebAppApiActions.Storage;

// Generated Code
namespace XTI_HubWebAppApi;
internal static partial class StorageGroupExtensions
{
    internal static void AddStorageServices(this IServiceCollection services)
    {
        services.AddScoped<GetStoredObjectAction>();
        services.AddScoped<GetStoredObjectValidation>();
        services.AddScoped<StoreObjectAction>();
        services.AddScoped<StoreObjectValidation>();
    }
}