using XTI_App;

namespace HubWebApp.Core
{
    public static class HubInfo
    {
        public static readonly AppKey AppKey = new AppKey("Hub", AppType.Values.WebApp);
        public static readonly HubRoles Roles = HubRoles.Instance;
        public static readonly HubModCategories ModCategories = HubModCategories.Instance;
    }
}
