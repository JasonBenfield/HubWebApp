using XTI_App;

namespace HubWebApp.Core
{
    public class HubModCategories
    {
        public static readonly HubModCategories Instance = new HubModCategories();

        private HubModCategories()
        {
            Apps = new ModifierCategoryName("Apps");
        }

        public ModifierCategoryName Apps { get; }
    }
}
