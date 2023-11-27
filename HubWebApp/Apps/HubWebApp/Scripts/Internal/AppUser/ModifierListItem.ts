import { BasicComponent } from "@jasonbenfield/sharedwebapp/Components/BasicComponent";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { ModifierButtonListItemView } from "./ModifierButtonListItemView";
import { Modifier } from "../../Lib/Modifier";

export class ModifierListItem extends BasicComponent {
    constructor(readonly modifier: Modifier, view: ModifierButtonListItemView) {
        super(view);
        new TextComponent(view.displayText).setText(modifier.displayText);
    }
}