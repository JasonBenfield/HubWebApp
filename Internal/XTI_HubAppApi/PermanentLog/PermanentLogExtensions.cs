using Microsoft.Extensions.DependencyInjection;
using XTI_HubAppApi.PermanentLog;

namespace XTI_HubAppApi
{
    internal static class PermanentLogExtensions
    {
        public static void AddPermanentLogGroupServices(this IServiceCollection services)
        {
            services.AddScoped<LogBatchAction>();
            services.AddScoped<StartSessionAction>();
            services.AddScoped<StartRequestAction>();
            services.AddScoped<AuthenticateSessionAction>();
            services.AddScoped<LogEventAction>();
            services.AddScoped<EndRequestAction>();
            services.AddScoped<EndSessionAction>();
            services.AddScoped<EndExpiredSessionsAction>();
        }
    }
}
