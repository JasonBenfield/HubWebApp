import { BasicComponent } from "@jasonbenfield/sharedwebapp/Components/BasicComponent";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { ModifierButtonListItemView } from "./ModifierButtonListItemView";

export class ModifierListItem extends BasicComponent {
    constructor(readonly modifier: IModifierModel, view: ModifierButtonListItemView) {
        super(view);
        new TextComponent(view.displayText).setText(modifier.DisplayText);
    }
}