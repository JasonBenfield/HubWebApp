import { BasicComponent } from "@jasonbenfield/sharedwebapp/Components/BasicComponent";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { ModifierCategoryListItemView } from "./ModifierCategoryListItemView";

export class ModifierCategoryListItem extends BasicComponent {
    constructor(readonly modCategory: IModifierCategoryModel, view: ModifierCategoryListItemView) {
        super(view);
        new TextComponent(view.categoryName).setText(modCategory.Name.DisplayText);
    }
}