import { BasicComponent } from "@jasonbenfield/sharedwebapp/Components/BasicComponent";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { ModifierListItemView } from "./ModifierListItemView";

export class ModifierListItem extends BasicComponent {
    constructor(modifier: IModifierModel, view: ModifierListItemView) {
        super(view);
        const modKey = new TextComponent(view.modKey);
        modKey.setText(modifier.ModKey.DisplayText);
        modKey.syncTitleWithText();
        const displayText = new TextComponent(view.displayText);
        displayText.setText(modifier.DisplayText);
        displayText.syncTitleWithText();
    }
}