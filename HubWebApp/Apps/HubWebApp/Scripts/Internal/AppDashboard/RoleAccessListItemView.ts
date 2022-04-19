import { FaIcon } from "@jasonbenfield/sharedwebapp/FaIcon";
import { Row } from "@jasonbenfield/sharedwebapp/Grid/Row";
import { TextSpanView } from "@jasonbenfield/sharedwebapp/Html/TextSpanView";
import { ListGroupItemView } from "@jasonbenfield/sharedwebapp/ListGroup/ListGroupItemView";

export class RoleAccessListItemView extends ListGroupItemView {
    readonly roleName: TextSpanView;

    constructor() {
        super();
        let row = this.addContent(new Row());
        this.roleName = row.addColumn()
            .addContent(new TextSpanView());
    }
}