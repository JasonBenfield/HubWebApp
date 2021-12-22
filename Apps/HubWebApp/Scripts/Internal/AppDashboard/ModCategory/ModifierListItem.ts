import { ModifierListItemView } from "./ModifierListItemView";

export class ModifierListItem {
    constructor(modifier: IModifierModel, view: ModifierListItemView) {
        view.setModKey(modifier.ModKey);
        view.setDisplayText(modifier.DisplayText);
    }
}