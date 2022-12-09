namespace XTI_SupportServiceAppApi;

public static class SupportAppApiExtensions
{
    public static void AddSupportAppApiServices(this IServiceCollection services)
    {
        services.AddPermanentLogGroupServices();
        services.AddInstallationsGroupServices();
    }
}