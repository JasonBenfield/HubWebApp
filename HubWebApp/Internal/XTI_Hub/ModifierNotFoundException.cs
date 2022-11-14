using XTI_App.Abstractions;
using XTI_App.Api;

namespace XTI_Hub
{
    public sealed class ModifierNotFoundException : AppException
    {
        private static readonly string displayMessage = "Modifier not found";

        public ModifierNotFoundException(int modifierID, App app)
            :base
            (
                $"Modifier {modifierID} not found for app '{app.ToModel().AppKey.Format()}'", 
                displayMessage
            )
        {
        }

        public ModifierNotFoundException(ModifierKey modKey, ModifierCategory category)
            : base
            (
                $"Modifier with mod key '{modKey.DisplayText}' not found for category '{category.ToModel().Name.DisplayText}'",
                displayMessage
            )
        {
        }

        public ModifierNotFoundException(string targetKey, ModifierCategory category)
            : base
            (
                $"Modifier with target key'{targetKey}' not found category '{category.ToModel().Name.DisplayText}'",
                displayMessage
            )
        {
        }
    }
}
