using XTI_HubAppApi.Apps;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XTI_App;
using XTI_App.Abstractions;
using XTI_App.Api;

namespace XTI_HubAppApi.Users
{
    public sealed class GetUserModifierCategoriesAction : AppAction<int, UserModifierCategoryModel[]>
    {
        private readonly AppFromPath appFromPath;
        private readonly AppFactory factory;

        public GetUserModifierCategoriesAction(AppFromPath appFromPath, AppFactory factory)
        {
            this.appFromPath = appFromPath;
            this.factory = factory;
        }

        public async Task<UserModifierCategoryModel[]> Execute(int userID)
        {
            var user = await factory.Users().User(userID);
            var app = await appFromPath.Value();
            var denyAccessRole = await app.Role(AppRoleName.DenyAccess);
            var modCategories = (await app.ModCategories())
                .Where(modCat => !modCat.Name().Equals(ModifierCategoryName.Default));
            var userModCategories = new List<UserModifierCategoryModel>();
            foreach (var modCategory in modCategories)
            {
                var userModCategory = new UserModifierCategoryModel();
                userModCategory.ModCategory = modCategory.ToModel();
                var modifiers = await modCategory.Modifiers();
                var modifierModels = new List<ModifierModel>();
                foreach (var modifier in modifiers)
                {
                    var assignedRoles = await user.AssignedRoles(modifier);
                    if (assignedRoles.Any() && !assignedRoles.Any(r => r.ID.Equals(denyAccessRole.ID)))
                    {
                        modifierModels.Add(modifier.ToModel());
                    }
                }
                userModCategory.Modifiers = modifierModels.ToArray();
                userModCategories.Add(userModCategory);
            }
            return userModCategories.ToArray();
        }
    }
}
