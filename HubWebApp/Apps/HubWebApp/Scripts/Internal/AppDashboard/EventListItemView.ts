import { TextCss } from "@jasonbenfield/sharedwebapp/TextCss";
import { ListGroupItemView } from "@jasonbenfield/sharedwebapp/Views/ListGroup";
import { RowView } from "@jasonbenfield/sharedwebapp/Views/RowView";
import { TextSpanView } from "@jasonbenfield/sharedwebapp/Views/TextSpanView";
import { BasicComponentView } from "../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Views/BasicComponentView";

export class EventListItemView extends ListGroupItemView {
    readonly timeOccurred: TextSpanView;
    readonly severity: TextSpanView;
    readonly caption: TextSpanView;
    readonly message: TextSpanView;

    constructor(container: BasicComponentView) {
        super(container);
        const row = this.addView(RowView);
        this.timeOccurred = row.addColumn()
            .addView(TextSpanView);
        this.severity = row.addColumn()
            .addView(TextSpanView);
        this.caption = row.addColumn()
            .configure(c => c.addCssFrom(new TextCss().truncate().cssClass()))
            .addView(TextSpanView);
        this.message = row.addColumn()
            .configure(c => c.addCssFrom(new TextCss().truncate().cssClass()))
            .addView(TextSpanView);
    }
}