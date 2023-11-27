import { BasicComponent } from "@jasonbenfield/sharedwebapp/Components/BasicComponent";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { ModifierListItemView } from "./ModifierListItemView";
import { Modifier } from "../../../Lib/Modifier";

export class ModifierListItem extends BasicComponent {
    constructor(modifier: Modifier, view: ModifierListItemView) {
        super(view);
        const modKey = new TextComponent(view.modKey);
        modKey.setText(modifier.modKey.displayText);
        modKey.syncTitleWithText();
        const displayText = new TextComponent(view.displayText);
        displayText.setText(modifier.displayText);
        displayText.syncTitleWithText();
    }
}