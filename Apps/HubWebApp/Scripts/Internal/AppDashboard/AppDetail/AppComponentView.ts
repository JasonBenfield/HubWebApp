import { CardTitleHeaderView } from "@jasonbenfield/sharedwebapp/Card/CardTitleHeaderView";
import { CardView } from '@jasonbenfield/sharedwebapp/Card/CardView';
import { ColumnCss } from "@jasonbenfield/sharedwebapp/ColumnCss";
import { Row } from "@jasonbenfield/sharedwebapp/Grid/Row";
import { TextSpan } from '@jasonbenfield/sharedwebapp/Html/TextSpan';
import { MessageAlertView } from "@jasonbenfield/sharedwebapp/MessageAlertView";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";

export class AppComponentView extends CardView {
    readonly alert: MessageAlertView;
    readonly titleHeader: CardTitleHeaderView;
    private readonly appName: TextSpan;
    private readonly appTitle: TextSpan;
    private readonly appType: TextSpan;

    constructor() {
        super();
        this.titleHeader = this.addCardTitleHeader();
        this.alert = this.addCardAlert().alert;
        let row = this.addCardBody()
            .addContent(new Row());
        this.appName = row.addColumn()
            .configure(c => c.setColumnCss(ColumnCss.xs('auto')))
            .addContent(new TextSpan());
        this.appTitle = row.addColumn()
            .configure(c => c.setColumnCss(ColumnCss.xs('auto')))
            .addContent(new TextSpan());
        this.appType = row.addColumn()
            .addContent(new TextSpan());
    }

    setAppName(appName: string) { this.appName.setText(appName); }

    setAppTitle(appTitle: string) { this.appTitle.setText(appTitle); }

    setAppType(appType: string) { this.appType.setText(appType); }
}