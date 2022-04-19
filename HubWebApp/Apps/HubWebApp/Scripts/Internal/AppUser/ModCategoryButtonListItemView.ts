import { TextBlockView } from "@jasonbenfield/sharedwebapp/Html/TextBlockView";
import { ButtonListGroupItemView } from "@jasonbenfield/sharedwebapp/ListGroup/ButtonListGroupItemView";

export class ModCategoryButtonListItemView extends ButtonListGroupItemView {
    readonly categoryName: TextBlockView;

    constructor() {
        super();
        this.categoryName = this.addContent(new TextBlockView);
    }
}