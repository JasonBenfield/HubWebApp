﻿using System.Linq;
using System.Threading.Tasks;
using XTI_App.Abstractions;

namespace XTI_HubAppClient
{
    internal sealed class HubClientApp : IApp
    {
        private readonly HubAppClient hubClient;
        private readonly HubClientAppContext appContext;
        public HubClientApp(HubAppClient hubClient, HubClientAppContext appContext, AppModel model)
        {
            this.hubClient = hubClient;
            this.appContext = appContext;
            ID = new EntityID(model.ID);
            Title = model.Title;
        }

        public EntityID ID { get; }
        public string Title { get; }

        public async Task<IModifierCategory> ModCategory(ModifierCategoryName name)
        {
            var modifier = await appContext.GetModifierKey();
            var modCategory = await hubClient.App.GetModifierCategory(modifier, name.Value);
            return new HubClientModifierCategory(hubClient, appContext, modCategory);
        }

        public async Task<IAppRole> Role(AppRoleName roleName)
        {
            var modifier = await appContext.GetModifierKey();
            var role = await hubClient.App.GetRole(modifier, roleName.Value);
            return new HubClientRole(role);
        }

        public async Task<IAppRole[]> Roles()
        {
            var modifier = await appContext.GetModifierKey();
            var roles = await hubClient.App.GetRoles(modifier);
            return roles.Select(r => new HubClientRole(r)).ToArray();
        }

        public async Task<IAppVersion> Version(AppVersionKey versionKey)
        {
            var modifier = await appContext.GetModifierKey();
            var version = await hubClient.Version.GetVersion(modifier, versionKey.Value);
            return new HubClientVersion(hubClient, appContext, version);
        }
    }
}