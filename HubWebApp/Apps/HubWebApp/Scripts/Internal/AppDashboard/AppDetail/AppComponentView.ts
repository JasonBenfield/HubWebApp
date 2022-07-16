import { CardTitleHeaderView, CardAlertView, CardView } from "@jasonbenfield/sharedwebapp/Views/Card";
import { ColumnCss } from "@jasonbenfield/sharedwebapp/ColumnCss";
import { RowView } from "@jasonbenfield/sharedwebapp/Views/RowView";
import { TextSpanView } from '@jasonbenfield/sharedwebapp/Views/TextSpanView';
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";

export class AppComponentView extends CardView {
    readonly alert: CardAlertView;
    readonly titleHeader: CardTitleHeaderView;
    readonly appName: TextSpanView;
    readonly appTitle: TextSpanView;
    readonly appType: TextSpanView;

    constructor(container: BasicComponentView) {
        super(container);
        this.titleHeader = this.addCardTitleHeader();
        this.alert = this.addCardAlert();
        const row = this.addCardBody()
            .addView(RowView);
        this.appName = row.addColumn()
            .configure(c => c.setColumnCss(ColumnCss.xs('auto')))
            .addView(TextSpanView);
        this.appTitle = row.addColumn()
            .configure(c => c.setColumnCss(ColumnCss.xs('auto')))
            .addView(TextSpanView);
        this.appType = row.addColumn()
            .addView(TextSpanView);
    }
}