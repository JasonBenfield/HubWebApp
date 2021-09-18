using XTI_App.Api;

namespace XTI_SupportAppApi
{
    public sealed class LogGroup : AppApiGroupWrapper
    {
        public LogGroup
        (
            AppApiGroup source,
            LogActionFactory actionFactory
        )
            : base(source)
        {
            var actions = new AppApiActionFactory(source);
            MoveToPermanent = source.AddAction
            (
                actions.Action
                (
                    nameof(MoveToPermanent),
                    actionFactory.CreateMoveToPermanent
                )
            );
            Decrypt = source.AddAction
            (
                actions.Action
                (
                    nameof(Decrypt),
                    actionFactory.CreateDecrypt
                )
            );
            Retry = source.AddAction
            (
                actions.Action
                (
                    nameof(Retry),
                    actionFactory.CreateRetry
                )
            );
        }

        public AppApiAction<EmptyRequest, EmptyActionResult> MoveToPermanent { get; }
        public AppApiAction<EmptyRequest, EmptyActionResult> Decrypt { get; }
        public AppApiAction<EmptyRequest, EmptyActionResult> Retry { get; }
    }
}
