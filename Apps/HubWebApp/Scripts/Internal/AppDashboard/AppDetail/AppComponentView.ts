﻿import { CardTitleHeaderView } from "@jasonbenfield/sharedwebapp/Card/CardTitleHeaderView";
import { CardView } from '@jasonbenfield/sharedwebapp/Card/CardView';
import { ColumnCss } from "@jasonbenfield/sharedwebapp/ColumnCss";
import { Row } from "@jasonbenfield/sharedwebapp/Grid/Row";
import { TextSpanView } from '@jasonbenfield/sharedwebapp/Html/TextSpanView';
import { MessageAlertView } from "@jasonbenfield/sharedwebapp/MessageAlertView";

export class AppComponentView extends CardView {
    readonly alert: MessageAlertView;
    readonly titleHeader: CardTitleHeaderView;
    readonly appName: TextSpanView;
    readonly appTitle: TextSpanView;
    readonly appType: TextSpanView;

    constructor() {
        super();
        this.titleHeader = this.addCardTitleHeader();
        this.alert = this.addCardAlert().alert;
        let row = this.addCardBody()
            .addContent(new Row());
        this.appName = row.addColumn()
            .configure(c => c.setColumnCss(ColumnCss.xs('auto')))
            .addContent(new TextSpanView());
        this.appTitle = row.addColumn()
            .configure(c => c.setColumnCss(ColumnCss.xs('auto')))
            .addContent(new TextSpanView());
        this.appType = row.addColumn()
            .addContent(new TextSpanView());
    }
}