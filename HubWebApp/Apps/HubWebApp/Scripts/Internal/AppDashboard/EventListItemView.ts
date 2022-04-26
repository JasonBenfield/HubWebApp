import { Row } from "@jasonbenfield/sharedwebapp/Grid/Row";
import { TextSpanView } from "@jasonbenfield/sharedwebapp/Html/TextSpanView";
import { ListGroupItemView } from "@jasonbenfield/sharedwebapp/ListGroup/ListGroupItemView";
import { TextCss } from "@jasonbenfield/sharedwebapp/TextCss";

export class EventListItemView extends ListGroupItemView {
    readonly timeOccurred: TextSpanView;
    readonly severity: TextSpanView;
    readonly caption: TextSpanView;
    readonly message: TextSpanView;

    constructor() {
        super();
        let row = this.addContent(new Row());
        this.timeOccurred = row.addColumn()
            .addContent(new TextSpanView());
        this.severity = row.addColumn()
            .addContent(new TextSpanView());
        this.caption = row.addColumn()
            .configure(c => c.addCssFrom(new TextCss().truncate().cssClass()))
            .addContent(new TextSpanView());
        this.message = row.addColumn()
            .configure(c => c.addCssFrom(new TextCss().truncate().cssClass()))
            .addContent(new TextSpanView());
    }
}