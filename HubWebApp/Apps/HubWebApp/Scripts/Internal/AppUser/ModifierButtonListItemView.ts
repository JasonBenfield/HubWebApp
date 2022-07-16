import { TextBlockView } from "@jasonbenfield/sharedwebapp/Views/TextBlockView";
import { ButtonListGroupItemView } from "@jasonbenfield/sharedwebapp/Views/ListGroup";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";

export class ModifierButtonListItemView extends ButtonListGroupItemView {
    readonly displayText: TextBlockView;

    constructor(container: BasicComponentView) {
        super(container);
        this.displayText = this.addView(TextBlockView);
    }
}