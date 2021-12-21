import { Row } from "@jasonbenfield/sharedwebapp/Grid/Row";
import { TextSpan } from "@jasonbenfield/sharedwebapp/Html/TextSpan";
import { ButtonListGroupItemView } from "@jasonbenfield/sharedwebapp/ListGroup/ButtonListGroupItemView";

export class ModifierCategoryListItemView extends ButtonListGroupItemView {
    private readonly categoryName: TextSpan;

    constructor() {
        super();
        this.categoryName = this.addContent(new Row())
            .addColumn()
            .addContent(new TextSpan());
    }

    setCategoryName(name: string) { this.categoryName.setText(name); }
}