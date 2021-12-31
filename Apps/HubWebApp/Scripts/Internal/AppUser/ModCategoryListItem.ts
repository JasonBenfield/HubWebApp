import { TextBlock } from "@jasonbenfield/sharedwebapp/Html/TextBlock";
import { ModCategoryButtonListItemView } from "./ModCategoryButtonListItemView";

export class ModCategoryListItem {
    constructor(
        readonly modCategory: IModifierCategoryModel,
        view: ModCategoryButtonListItemView
    ) {
        new TextBlock(modCategory.Name, view.categoryName);
    }
}