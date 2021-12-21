using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                $"Modifier {modifierID} not found for app '{app.Key().Serialize()}'", 
                displayMessage
            )
        {
        }

        public ModifierNotFoundException(ModifierKey modKey, ModifierCategory category)
            : base
            (
                $"Modifier with mod key '{modKey.DisplayText}' not found category '{category.Name().DisplayText}'",
                displayMessage
            )
        {
        }

        public ModifierNotFoundException(string targetKey, ModifierCategory category)
            : base
            (
                $"Modifier with target key'{targetKey}' not found category '{category.Name().DisplayText}'",
                displayMessage
            )
        {
        }
    }
}
