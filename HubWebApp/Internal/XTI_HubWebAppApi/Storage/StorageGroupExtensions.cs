using XTI_HubWebAppApi.Storage;

namespace XTI_HubWebAppApi;

public static class StorageGroupExtensions
{
    public static void AddStorageGroupServices(this IServiceCollection services)
    {
        services.AddScoped<StoreObjectValidation>();
        services.AddScoped<StoreObjectAction>();
        services.AddScoped<GetStoredObjectValidation>();
        services.AddScoped<GetStoredObjectAction>();
    }
}
