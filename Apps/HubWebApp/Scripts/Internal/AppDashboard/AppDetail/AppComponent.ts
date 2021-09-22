import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { Card } from 'XtiShared/Card/Card';
import { BlockViewModel } from "XtiShared/Html/BlockViewModel";
import { MessageAlert } from "XtiShared/MessageAlert";
import { Row } from "XtiShared/Grid/Row";
import { TextSpan } from 'XtiShared/Html/TextSpan';
import { ColumnCss } from "XtiShared/ColumnCss";

export class AppComponent extends Card {
    constructor(
        private readonly hubApi: HubAppApi,
        vm: BlockViewModel = new BlockViewModel()
    ) {
        super();
        this.addCardTitleHeader('App');
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

    private readonly alert: MessageAlert;
    private readonly appName: TextSpan;
    private readonly appTitle: TextSpan;
    private readonly appType: TextSpan;

    async refresh() {
        let app = await this.getApp();
        this.appName.setText(app.AppName);
        this.appTitle.setText(app.Title);
        this.appType.setText(app.Type.DisplayText);
    }

    private async getApp() {
        let app: IAppModel;
        await this.alert.infoAction(
            'Loading...',
            async () => {
                app = await this.hubApi.App.GetApp();
            }
        );
        return app;
    }
}