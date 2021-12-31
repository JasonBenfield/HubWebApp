import { ColumnCss } from "@jasonbenfield/sharedwebapp/ColumnCss";
import { Row } from "@jasonbenfield/sharedwebapp/Grid/Row";
import { TextSpanView } from "@jasonbenfield/sharedwebapp/Html/TextSpanView";
import { ButtonListGroupItemView } from "@jasonbenfield/sharedwebapp/ListGroup/ButtonListGroupItemView";

export class ResourceListItemView extends ButtonListGroupItemView {
    readonly resourceName: TextSpanView;
    readonly resultType: TextSpanView;

    constructor() {
        super();
        let row = this.addContent(new Row());
        this.resourceName = row.addColumn()
            .configure(c => c.setColumnCss(ColumnCss.xs(8)))
            .addContent(new TextSpanView());
        this.resultType = row.addColumn()
            .addContent(new TextSpanView());
    }
}