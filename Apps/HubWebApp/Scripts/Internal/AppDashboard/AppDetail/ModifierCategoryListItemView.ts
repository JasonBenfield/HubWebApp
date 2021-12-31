import { Row } from "@jasonbenfield/sharedwebapp/Grid/Row";
import { TextSpanView } from "@jasonbenfield/sharedwebapp/Html/TextSpanView";
import { ButtonListGroupItemView } from "@jasonbenfield/sharedwebapp/ListGroup/ButtonListGroupItemView";

export class ModifierCategoryListItemView extends ButtonListGroupItemView {
    readonly categoryName: TextSpanView;

    constructor() {
        super();
        this.categoryName = this.addContent(new Row())
            .addColumn()
            .addContent(new TextSpanView());
    }
}