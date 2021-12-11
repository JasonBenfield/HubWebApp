using XTI_App.Abstractions;

namespace XTI_Hub
{ 
    public sealed class HubModCategories
    {
        internal static readonly HubModCategories Instance = new HubModCategories();

        private HubModCategories()
        {
            Default = ModifierCategoryName.Default;
            Apps = new ModifierCategoryName("Apps");
        }

        public ModifierCategoryName Default { get; }
        public ModifierCategoryName Apps { get; }
    }
}
