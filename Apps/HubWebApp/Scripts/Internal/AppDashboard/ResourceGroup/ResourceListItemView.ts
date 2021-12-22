import { ColumnCss } from "@jasonbenfield/sharedwebapp/ColumnCss";
import { Row } from "@jasonbenfield/sharedwebapp/Grid/Row";
import { TextSpan } from "@jasonbenfield/sharedwebapp/Html/TextSpan";
import { ButtonListGroupItemView } from "@jasonbenfield/sharedwebapp/ListGroup/ButtonListGroupItemView";

export class ResourceListItemView extends ButtonListGroupItemView {
    private readonly resourceName: TextSpan;
    private readonly resultType: TextSpan;

    constructor() {
        super();
        let row = this.addContent(new Row());
        this.resourceName = row.addColumn()
            .configure(c => c.setColumnCss(ColumnCss.xs(8)))
            .addContent(new TextSpan());
        this.resultType = row.addColumn()
            .addContent(new TextSpan());
    }

    setResourceName(name: string) { this.resourceName.setText(name); }

    setResultType(resultType: string) { this.resultType.setText(resultType); }
}