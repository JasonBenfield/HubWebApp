using XTI_App;

namespace HubWebApp.Core
{
    public class HubModCategories
    {
        public static readonly HubModCategories Instance = new HubModCategories();

        private HubModCategories()
        {
            Default = ModifierCategoryName.Default;
            Apps = new ModifierCategoryName("Apps");
        }

        public ModifierCategoryName Default { get; }
        public ModifierCategoryName Apps { get; }
    }
}
