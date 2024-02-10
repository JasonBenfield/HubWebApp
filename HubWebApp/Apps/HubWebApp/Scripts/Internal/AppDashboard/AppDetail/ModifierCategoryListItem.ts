import { BasicComponent } from "@jasonbenfield/sharedwebapp/Components/BasicComponent";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { ModifierCategoryListItemView } from "./ModifierCategoryListItemView";
import { ModifierCategory } from "../../../Lib/ModifierCategory";

export class ModifierCategoryListItem extends BasicComponent {
    constructor(readonly modCategory: ModifierCategory, view: ModifierCategoryListItemView) {
        super(view);
        new TextComponent(view.categoryName).setText(modCategory.name.displayText);
    }
}