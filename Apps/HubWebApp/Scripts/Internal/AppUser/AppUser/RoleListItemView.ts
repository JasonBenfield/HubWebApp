import { Row } from "@jasonbenfield/sharedwebapp/Grid/Row";
import { TextSpan } from "@jasonbenfield/sharedwebapp/Html/TextSpan";
import { ListGroupItemView } from "@jasonbenfield/sharedwebapp/ListGroup/ListGroupItemView";

export class RoleListItemView extends ListGroupItemView {
    private readonly roleName: TextSpan;

    constructor() {
        super();
        this.roleName = this.addContent(new Row())
            .addColumn()
            .addContent(new TextSpan());
    }

    setRoleName(roleName: string) { this.roleName.setText(roleName); }
}