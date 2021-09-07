using System;
using System.Threading.Tasks;
using XTI_App;
using XTI_App.Abstractions;

namespace XTI_HubAppApi
{
    public sealed class AppFromPath
    {
        private readonly AppFactory factory;
        private readonly XtiPath path;

        public AppFromPath(AppFactory factory, XtiPath path)
        {
            this.factory = factory;
            this.path = path;
        }

        public async Task<App> Value()
        {
            var modKey = path.Modifier;
            if (modKey.Equals(ModifierKey.Default))
            {
                throw new Exception(AppErrors.ModifierIsRequired);
            }
            var hubApp = await factory.Apps().App(HubInfo.AppKey);
            var modCategory = await hubApp.ModCategory(HubInfo.ModCategories.Apps);
            var modifier = await modCategory.Modifier(modKey);
            if (!modifier.ModKey().Equals(modKey))
            {
                throw new Exception(string.Format(AppErrors.ModifierNotFound, modKey.DisplayText));
            }
            var app = await factory.Apps().App(modifier.TargetID());
            return app;
        }
    }
}
