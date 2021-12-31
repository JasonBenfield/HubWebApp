import { Row } from "@jasonbenfield/sharedwebapp/Grid/Row";
import { TextSpanView } from "@jasonbenfield/sharedwebapp/Html/TextSpanView";
import { ButtonListGroupItemView } from "@jasonbenfield/sharedwebapp/ListGroup/ButtonListGroupItemView";

export class ResourceGroupListItemView extends ButtonListGroupItemView {
    readonly groupName: TextSpanView;

    constructor() {
        super();
        let row = new Row();
        this.groupName = row.addColumn()
            .addContent(new TextSpanView());
    }
}