﻿using XTI_SupportServiceAppApi.PermanentLog;

namespace XTI_SupportServiceAppApi;

internal static class PermanentLogGroupExtensions
{
    public static void AddPermanentLogGroupServices(this IServiceCollection services)
    {
        services.AddScoped<MoveToPermanentAction>();
        services.AddScoped<MoveToPermanentV1Action>();
        services.AddScoped<RetryAction>();
        services.AddScoped<RetryV1Action>();
    }
}