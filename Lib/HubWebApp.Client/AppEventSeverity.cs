// Generated Code
using XTI_WebAppClient;

namespace HubWebApp.Client
{
    public sealed class AppEventSeverity : NumericValue
    {
        public sealed class AppEventSeveritys : NumericValues<AppEventSeverity>
        {
            internal AppEventSeveritys()
            {
                NotSet = Add(new AppEventSeverity(0, "Not Set"));
                CriticalError = Add(new AppEventSeverity(100, "Critical Error"));
                AccessDenied = Add(new AppEventSeverity(80, "Access Denied"));
                AppError = Add(new AppEventSeverity(70, "App Error"));
                ValidationFailed = Add(new AppEventSeverity(60, "Validation Failed"));
            }

            public AppEventSeverity NotSet { get; }

            public AppEventSeverity CriticalError { get; }

            public AppEventSeverity AccessDenied { get; }

            public AppEventSeverity AppError { get; }

            public AppEventSeverity ValidationFailed { get; }
        }

        public static readonly AppEventSeveritys Values = new AppEventSeveritys();
        public AppEventSeverity(int value, string displayText): base(value, displayText)
        {
        }
    }
}