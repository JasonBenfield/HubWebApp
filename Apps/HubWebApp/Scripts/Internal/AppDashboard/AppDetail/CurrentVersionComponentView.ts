import { CardTitleHeaderView } from "@jasonbenfield/sharedwebapp/Card/CardTitleHeaderView";
import { CardView } from "@jasonbenfield/sharedwebapp/Card/CardView";
import { ColumnCss } from "@jasonbenfield/sharedwebapp/ColumnCss";
import { Row } from "@jasonbenfield/sharedwebapp/Grid/Row";
import { TextSpan } from "@jasonbenfield/sharedwebapp/Html/TextSpan";
import { MessageAlertView } from "@jasonbenfield/sharedwebapp/MessageAlertView";

export class CurrentVersionComponentView extends CardView {
    readonly titleHeader: CardTitleHeaderView;
    readonly alert: MessageAlertView;
    private readonly versionKey: TextSpan;
    private readonly version: TextSpan;

    constructor() {
        super();
        this.titleHeader = this.addCardTitleHeader();
        this.alert = this.addCardAlert().alert;
        let row = this.addCardBody()
            .addContent(new Row());
        this.versionKey = row.addColumn()
            .configure(c => c.setColumnCss(ColumnCss.xs('auto')))
            .addContent(new TextSpan());
        this.version = row.addColumn()
            .addContent(new TextSpan());
    }

    setVersionKey(versionKey: string) { this.versionKey.setText(versionKey); }

    setVersion(version: string) { this.version.setText(version); }
} 