import { TextBlockView } from "@jasonbenfield/sharedwebapp/Html/TextBlockView";
import { ButtonListGroupItemView } from "@jasonbenfield/sharedwebapp/ListGroup/ButtonListGroupItemView";

export class ModifierButtonListItemView extends ButtonListGroupItemView {
    readonly displayText: TextBlockView;

    constructor() {
        super();
        this.displayText = this.addContent(new TextBlockView);
    }
}