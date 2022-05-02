import { ColumnCss } from "@jasonbenfield/sharedwebapp/ColumnCss";
import { Row } from "@jasonbenfield/sharedwebapp/Grid/Row";
import { TextSpanView } from "@jasonbenfield/sharedwebapp/Html/TextSpanView";
import { ButtonListGroupItemView } from "@jasonbenfield/sharedwebapp/ListGroup/ButtonListGroupItemView";

export class UserListItemView extends ButtonListGroupItemView {
    readonly userName: TextSpanView;
    readonly fullName: TextSpanView;

    constructor() {
        super();
        let row = this.addContent(new Row());
        this.userName = row.addColumn()
            .configure(c => c.setColumnCss(ColumnCss.xs(4)))
            .addContent(new TextSpanView());
        this.fullName = row.addColumn()
            .addContent(new TextSpanView());
    }
}