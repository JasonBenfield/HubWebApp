import { ModifierCategoryListItemView } from "./ModifierCategoryListItemView";

export class ModifierCategoryListItem {
    constructor(readonly modCategory: IModifierCategoryModel, view: ModifierCategoryListItemView) {
        view.setCategoryName(modCategory.Name);
    }
}