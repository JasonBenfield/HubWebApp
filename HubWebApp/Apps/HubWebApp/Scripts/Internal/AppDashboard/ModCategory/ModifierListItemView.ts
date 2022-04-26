import { ColumnCss } from "@jasonbenfield/sharedwebapp/ColumnCss";
import { Row } from "@jasonbenfield/sharedwebapp/Grid/Row";
import { TextSpanView } from "@jasonbenfield/sharedwebapp/Html/TextSpanView";
import { ListGroupItemView } from "@jasonbenfield/sharedwebapp/ListGroup/ListGroupItemView";
import { TextCss } from "@jasonbenfield/sharedwebapp/TextCss";

export class ModifierListItemView extends ListGroupItemView {
    readonly modKey: TextSpanView;
    readonly displayText: TextSpanView;

    constructor() {
        super();
        let row = this.addContent(new Row());
        this.modKey = row.addColumn()
            .configure(c => {
                c.setColumnCss(ColumnCss.xs(4));
                c.addCssFrom(new TextCss().truncate().cssClass());
            })
            .addContent(new TextSpanView());
        this.displayText = row.addColumn()
            .configure(c => c.addCssFrom(new TextCss().truncate().cssClass()))
            .addContent(new TextSpanView());
    }
}