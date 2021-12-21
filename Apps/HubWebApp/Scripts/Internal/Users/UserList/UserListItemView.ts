import { ColumnCss } from "@jasonbenfield/sharedwebapp/ColumnCss";
import { Row } from "@jasonbenfield/sharedwebapp/Grid/Row";
import { TextSpan } from "@jasonbenfield/sharedwebapp/Html/TextSpan";
import { ButtonListGroupItemView } from "@jasonbenfield/sharedwebapp/ListGroup/ButtonListGroupItemView";

export class UserListItemView extends ButtonListGroupItemView {
    private readonly userName: TextSpan;
    private readonly fullName: TextSpan;

    constructor() {
        super();
        let row = this.addContent(new Row());
        this.userName = row.addColumn()
            .configure(c => c.setColumnCss(ColumnCss.xs(4)))
            .addContent(new TextSpan());
        this.fullName = row.addColumn()
            .addContent(new TextSpan());
    }

    setUserName(userName: string) { this.userName.setText(userName); }

    setFullName(fullName: string) { this.fullName.setText(fullName); }
}