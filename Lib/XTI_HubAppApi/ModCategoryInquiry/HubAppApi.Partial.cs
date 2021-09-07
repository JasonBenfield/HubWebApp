using System;
using XTI_HubAppApi.ModCategoryInquiry;

namespace XTI_HubAppApi
{
    partial class HubAppApi
    {
        public ModCategoryGroup ModCategory { get; private set; }

        partial void modCategory(IServiceProvider services)
        {
            ModCategory = new ModCategoryGroup
            (
                source.AddGroup
                (
                    nameof(ModCategory),
                    HubInfo.ModCategories.Apps,
                    Access.WithAllowed(HubInfo.Roles.ViewApp)
                ),
                new ModCategoryGroupActionFactory(services)
            );
        }
    }
}
