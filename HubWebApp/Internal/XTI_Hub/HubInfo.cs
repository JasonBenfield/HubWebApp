using XTI_App.Abstractions;

namespace XTI_Hub;

public static class HubInfo
{
    public static readonly AppKey AppKey = AppKey.WebApp("Hub");
    public static readonly HubRoles Roles = HubRoles.Instance;
    public static readonly HubModCategories ModCategories = HubModCategories.Instance;
}