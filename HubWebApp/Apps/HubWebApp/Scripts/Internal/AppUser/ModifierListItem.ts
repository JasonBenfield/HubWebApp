import { BasicComponent } from "../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Components/BasicComponent";
import { TextComponent } from "../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Components/TextComponent";
import { ModifierButtonListItemView } from "./ModifierButtonListItemView";

export class ModifierListItem extends BasicComponent {
    constructor(readonly modifier: IModifierModel, view: ModifierButtonListItemView) {
        super(view);
        new TextComponent(view.displayText).setText(modifier.DisplayText);
    }
}