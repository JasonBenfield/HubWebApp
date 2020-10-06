using XTI_App;

namespace HubWebApp.Core
{
    public sealed class HubAppKey
    {
        public static readonly string Value = "Hub";
        public static readonly AppKey Key = new AppKey(Value);
    }
}
