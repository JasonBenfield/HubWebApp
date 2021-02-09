import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { Card } from "XtiShared/Card/Card";
import { BlockViewModel } from "XtiShared/Html/BlockViewModel";
import { MessageAlert } from "XtiShared/MessageAlert";
import { TextSpan } from "XtiShared/Html/TextSpan";
import { Row } from "XtiShared/Grid/Row";
import { ColumnCss } from "XtiShared/ColumnCss";

export class CurrentVersionComponent extends Card {
    constructor(
        private readonly hubApi: HubAppApi, 
        vm: BlockViewModel = new BlockViewModel()
    ) {
        super(vm);
        this.addCardTitleHeader('Version');
        this.alert = this.addCardAlert().alert;
        let row = this.addCardBody()
            .addContent(new Row());
        this.versionKey = row.addColumn()
            .configure(c => c.setColumnCss(ColumnCss.xs('auto')))
            .addContent(new TextSpan());
        this.version = row.addColumn()
            .addContent(new TextSpan());
    }

    private readonly alert: MessageAlert;
    private readonly versionKey: TextSpan;
    private readonly version: TextSpan;

    async refresh() {
        let currentVersion = await this.getCurrentVersion();
        this.versionKey.setText(currentVersion.VersionKey);
        this.version.setText(`${currentVersion.Major}.${currentVersion.Minor}.${currentVersion.Patch}`);
    }

    private async getCurrentVersion() {
        let currentVersion: IAppVersionModel;
        await this.alert.infoAction(
            'Loading...',
            async () => {
                currentVersion = await this.hubApi.App.GetCurrentVersion();
            }
        );
        return currentVersion;
    }
} 