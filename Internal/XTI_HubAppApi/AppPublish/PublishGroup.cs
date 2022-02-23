using Microsoft.Extensions.DependencyInjection;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_Hub.Abstractions;
using XTI_WebApp.Api;

namespace XTI_HubAppApi.AppPublish;

public sealed class PublishGroup : AppApiGroupWrapper
{
    public PublishGroup(AppApiGroup source, IServiceProvider services)
        : base(source)
    {
        var actions = new WebAppApiActionFactory(source);
        NextVersionKey = source.AddAction
        (
            actions.Action
            (
                nameof(NextVersionKey),
                () => services.GetRequiredService<NextVersionKeyAction>()
            )
        );
        NewVersion = source.AddAction
        (
            actions.Action
            (
                nameof(NewVersion),
                () => services.GetRequiredService<NewVersionValidation>(),
                () => services.GetRequiredService<NewVersionAction>()
            )
        );
        BeginPublish = source.AddAction(actions.Action(nameof(BeginPublish), () => services.GetRequiredService<BeginPublishAction>()));
        EndPublish = source.AddAction(actions.Action(nameof(EndPublish), () => services.GetRequiredService<EndPublishAction>()));
        GetVersions = source.AddAction(actions.Action(nameof(GetVersions), () => services.GetRequiredService<GetVersionsAction>()));
    }

    public AppApiAction<EmptyRequest, AppVersionKey> NextVersionKey { get; }
    public AppApiAction<NewVersionRequest, AppVersionModel> NewVersion { get; }
    public AppApiAction<PublishVersionRequest, AppVersionModel> BeginPublish { get; }
    public AppApiAction<PublishVersionRequest, AppVersionModel> EndPublish { get; }
    public AppApiAction<AppKey, AppVersionModel[]> GetVersions { get; }
}