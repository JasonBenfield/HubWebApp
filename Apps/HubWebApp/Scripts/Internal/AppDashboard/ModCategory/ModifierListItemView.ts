import { ColumnCss } from "@jasonbenfield/sharedwebapp/ColumnCss";
import { Row } from "@jasonbenfield/sharedwebapp/Grid/Row";
import { TextSpan } from "@jasonbenfield/sharedwebapp/Html/TextSpan";
import { ListGroupItemView } from "@jasonbenfield/sharedwebapp/ListGroup/ListGroupItemView";
import { TextCss } from "@jasonbenfield/sharedwebapp/TextCss";

export class ModifierListItemView extends ListGroupItemView {
    private readonly modKey: TextSpan;
    private readonly displayText: TextSpan;

    constructor() {
        super();
        let row = this.addContent(new Row());
        this.modKey = row.addColumn()
            .configure(c => {
                c.setColumnCss(ColumnCss.xs(4));
                c.addCssFrom(new TextCss().truncate().cssClass());
            })
            .addContent(new TextSpan())
            .configure(ts => ts.syncTitleWithText());
        this.displayText = row.addColumn()
            .configure(c => c.addCssFrom(new TextCss().truncate().cssClass()))
            .addContent(new TextSpan())
            .configure(ts => ts.syncTitleWithText());
    }

    setModKey(modKey: string) { this.modKey.setText(modKey); }

    setDisplayText(displayText: string) { this.displayText.setText(displayText); }
}