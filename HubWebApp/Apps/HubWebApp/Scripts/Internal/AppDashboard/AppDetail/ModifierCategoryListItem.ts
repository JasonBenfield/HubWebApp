import { TextBlock } from "@jasonbenfield/sharedwebapp/Html/TextBlock";
import { ModifierCategoryListItemView } from "./ModifierCategoryListItemView";

export class ModifierCategoryListItem {
    constructor(readonly modCategory: IModifierCategoryModel, view: ModifierCategoryListItemView) {
        new TextBlock(modCategory.Name, view.categoryName);
    }
}