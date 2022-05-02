using XTI_HubAppApi.AppList;

namespace XTI_HubAppApi
{
    partial class HubAppApi
    {
        private AppListGroup? apps;
        public AppListGroup Apps { get => apps ?? throw new ArgumentNullException(nameof(apps)); }

        partial void createAppList(IServiceProvider sp)
        {
            apps = new AppListGroup
            (
                source.AddGroup(nameof(Apps), ResourceAccess.AllowAuthenticated()),
                sp
            );
        }
    }
}
