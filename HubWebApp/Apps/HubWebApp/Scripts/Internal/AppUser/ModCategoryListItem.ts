import { BasicComponent } from "@jasonbenfield/sharedwebapp/Components/BasicComponent";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { ModCategoryButtonListItemView } from "./ModCategoryButtonListItemView";
import { ModifierCategory } from "../../Lib/ModifierCategory";

export class ModCategoryListItem extends BasicComponent {
    constructor(
        readonly modCategory: ModifierCategory,
        view: ModCategoryButtonListItemView
    ) {
        super(view);
        new TextComponent(view.categoryName).setText(modCategory.name.displayText);
    }
}