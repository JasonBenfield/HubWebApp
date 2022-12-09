using Microsoft.Extensions.DependencyInjection;
using XTI_App.Api;
using XTI_SupportServiceAppApi;

namespace SupportServiceAppIntegrationTests;

internal static class SupportActionTester
{
    public static SupportActionTester<TModel, TResult> Create<TModel, TResult>(IServiceProvider services, Func<SupportAppApi, AppApiAction<TModel, TResult>> getAction)
    {
        return new SupportActionTester<TModel, TResult>(services, getAction);
    }
}

internal interface ISupportActionTester
{
    IServiceProvider Services { get; }
    SupportActionTester<TOtherModel, TOtherResult> Create<TOtherModel, TOtherResult>(Func<SupportAppApi, AppApiAction<TOtherModel, TOtherResult>> getAction);
}

internal sealed class SupportActionTester<TModel, TResult> : ISupportActionTester
{
    private readonly Func<SupportAppApi, AppApiAction<TModel, TResult>> getAction;

    public SupportActionTester
    (
        IServiceProvider services,
        Func<SupportAppApi, AppApiAction<TModel, TResult>> getAction
    )
    {
        Services = services;
        this.getAction = getAction;
    }

    public SupportActionTester<TOtherModel, TOtherResult> Create<TOtherModel, TOtherResult>
    (
        Func<SupportAppApi, AppApiAction<TOtherModel, TOtherResult>> getAction
    )
    {
        return SupportActionTester.Create(Services, getAction);
    }

    public IServiceProvider Services { get; }
    
    public async Task<TResult> Execute(TModel model)
    {
        var appApi = Services.GetRequiredService<SupportAppApi>();
        var action = getAction(appApi);
        var result = await action.Invoke(model);
        return result;
    }
}