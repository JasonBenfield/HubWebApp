import { Row } from "@jasonbenfield/sharedwebapp/Grid/Row";
import { TextSpan } from "@jasonbenfield/sharedwebapp/Html/TextSpan";
import { ButtonListGroupItemView } from "@jasonbenfield/sharedwebapp/ListGroup/ButtonListGroupItemView";

export class ResourceGroupListItemView extends ButtonListGroupItemView {
    private readonly groupName: TextSpan;

    constructor() {
        super();
        let row = new Row();
        this.groupName = row.addColumn()
            .addContent(new TextSpan());
    }

    setGroupName(groupName: string) { this.groupName.setText(groupName); }
}