import { TextBlock } from "@jasonbenfield/sharedwebapp/Html/TextBlock";
import { ModifierListItemView } from "./ModifierListItemView";

export class ModifierListItem {
    constructor(modifier: IModifierModel, view: ModifierListItemView) {
        let modKey = new TextBlock(modifier.ModKey, view.modKey);
        modKey.syncTitleWithText();
        let displayText = new TextBlock(modifier.DisplayText, view.displayText);
        displayText.syncTitleWithText();
    }
}