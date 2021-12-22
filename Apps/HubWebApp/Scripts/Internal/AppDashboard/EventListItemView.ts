import { Row } from "@jasonbenfield/sharedwebapp/Grid/Row";
import { TextSpan } from "@jasonbenfield/sharedwebapp/Html/TextSpan";
import { ListGroupItemView } from "@jasonbenfield/sharedwebapp/ListGroup/ListGroupItemView";
import { TextCss } from "@jasonbenfield/sharedwebapp/TextCss";

export class EventListItemView extends ListGroupItemView {
    private readonly timeOccurred: TextSpan;
    private readonly severity: TextSpan;
    private readonly caption: TextSpan;
    private readonly message: TextSpan;

    constructor() {
        super();
        let row = this.addContent(new Row());
        this.timeOccurred = row.addColumn()
            .addContent(new TextSpan());
        this.severity = row.addColumn()
            .addContent(new TextSpan());
        this.caption = row.addColumn()
            .configure(c => c.addCssFrom(new TextCss().truncate().cssClass()))
            .addContent(new TextSpan());
            this.caption.syncTitleWithText();
        this.message = row.addColumn()
            .configure(c => c.addCssFrom(new TextCss().truncate().cssClass()))
            .addContent(new TextSpan());
        this.message.syncTitleWithText();
    }

    setTimeOccurred(timeOccurred: string) { this.timeOccurred.setText(timeOccurred); }

    setSeverity(severity: string) { this.severity.setText(severity); }

    setCaption(caption: string) { this.caption.setText(caption); }

    setMessage(message: string) { this.message.setText(message); }
}