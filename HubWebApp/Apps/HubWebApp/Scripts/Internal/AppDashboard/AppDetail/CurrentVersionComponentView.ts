import { ColumnCss } from "@jasonbenfield/sharedwebapp/ColumnCss";
import { CardTitleHeaderView, CardView } from "@jasonbenfield/sharedwebapp/Views/Card";
import { MessageAlertView } from "@jasonbenfield/sharedwebapp/Views/MessageAlertView";
import { RowView } from "@jasonbenfield/sharedwebapp/Views/RowView";
import { TextSpanView } from "@jasonbenfield/sharedwebapp/Views/TextSpanView";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";

export class CurrentVersionComponentView extends CardView {
    readonly titleHeader: CardTitleHeaderView;
    readonly alert: MessageAlertView;
    readonly versionKey: TextSpanView;
    readonly version: TextSpanView;

    constructor(container: BasicComponentView) {
        super(container);
        this.titleHeader = this.addCardTitleHeader();
        this.alert = this.addCardAlert().alert;
        const row = this.addCardBody()
            .addView(RowView);
        this.versionKey = row.addColumn()
            .configure(c => c.setColumnCss(ColumnCss.xs('auto')))
            .addView(TextSpanView);
        this.version = row.addColumn()
            .addView(TextSpanView);
    }
} 