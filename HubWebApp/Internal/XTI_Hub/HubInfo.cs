using XTI_App.Abstractions;

namespace XTI_Hub;

public static class HubInfo
{
    public static readonly AppKey AppKey = new AppKey("Hub", AppType.Values.WebApp);
    public static readonly HubRoles Roles = HubRoles.Instance;
    public static readonly HubModCategories ModCategories = HubModCategories.Instance;
}