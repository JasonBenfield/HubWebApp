import { BasicComponent } from "@jasonbenfield/sharedwebapp/Components/BasicComponent";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { ModCategoryButtonListItemView } from "./ModCategoryButtonListItemView";

export class ModCategoryListItem extends BasicComponent {
    constructor(
        readonly modCategory: IModifierCategoryModel,
        view: ModCategoryButtonListItemView
    ) {
        super(view);
        new TextComponent(view.categoryName).setText(modCategory.Name.DisplayText);
    }
}