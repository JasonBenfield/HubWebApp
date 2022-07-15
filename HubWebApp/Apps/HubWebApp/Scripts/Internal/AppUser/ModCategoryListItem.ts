import { BasicComponent } from "../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Components/BasicComponent";
import { TextComponent } from "../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Components/TextComponent";
import { ModCategoryButtonListItemView } from "./ModCategoryButtonListItemView";

export class ModCategoryListItem extends BasicComponent {
    constructor(
        readonly modCategory: IModifierCategoryModel,
        view: ModCategoryButtonListItemView
    ) {
        super(view);
        new TextComponent(view.categoryName).setText(modCategory.Name);
    }
}