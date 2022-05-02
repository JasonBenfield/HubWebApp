import { TextBlock } from "@jasonbenfield/sharedwebapp/Html/TextBlock";
import { ModifierButtonListItemView } from "./ModifierButtonListItemView";

export class ModifierListItem {
    constructor(readonly modifier: IModifierModel, view: ModifierButtonListItemView) {
        new TextBlock(modifier.DisplayText, view.displayText);
    }
}