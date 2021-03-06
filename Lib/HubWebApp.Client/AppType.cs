// Generated Code
using XTI_WebAppClient;

namespace HubWebApp.Client
{
    public sealed class AppType : NumericValue
    {
        public sealed class AppTypes : NumericValues<AppType>
        {
            internal AppTypes()
            {
                NotFound = Add(new AppType(0, "Not Found"));
                WebApp = Add(new AppType(10, "Web App"));
                Service = Add(new AppType(15, "Service"));
                Package = Add(new AppType(20, "Package"));
                ConsoleApp = Add(new AppType(25, "Console App"));
            }

            public AppType NotFound
            {
                get;
            }

            public AppType WebApp
            {
                get;
            }

            public AppType Service
            {
                get;
            }

            public AppType Package
            {
                get;
            }

            public AppType ConsoleApp
            {
                get;
            }
        }

        public static readonly AppTypes Values = new AppTypes();
        public AppType(int value, string displayText): base(value, displayText)
        {
        }
    }
}