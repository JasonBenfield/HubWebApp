import { Card } from "XtiShared/Card/Card";
import { DefaultEvent } from "XtiShared/Events";
import { Row } from "XtiShared/Grid/Row";
import { BlockViewModel } from "XtiShared/Html/BlockViewModel";
import { TextSpan } from "XtiShared/Html/TextSpan";
import { MessageAlert } from "XtiShared/MessageAlert";
import { CardLinkListGroup } from "XtiShared/Card/CardLinkListGroup";
import { HubAppApi } from "../../Hub/Api/HubAppApi";

export class AppListCard extends Card {
    constructor(
        private readonly hubApi: HubAppApi,
        private readonly appRedirectUrl: (appID: number)=> string,
        vm: BlockViewModel = new BlockViewModel()
    ) {
        super(vm);
        this.addCardTitleHeader('Apps');
        this.alert = this.addCardAlert().alert;
        this.apps = this.addLinkListGroup();
        this.apps.itemClicked.register(this.onAppSelected.bind(this))
    }

    private onAppSelected(listItem: IListItem) {
        this._appSelected.invoke(listItem.getData<IAppModel>())
    }

    private readonly alert: MessageAlert;
    private readonly apps: CardLinkListGroup;

    private readonly _appSelected = new DefaultEvent<IAppModel>(this);
    readonly appSelected = this._appSelected.handler();

    async refresh() {
        let apps = await this.getApps();
        this.apps.setItems(
            apps,
            (sourceItem, listItem) => {
                let row = listItem.addContent(new Row());
                row.addColumn()
                    .addContent(new TextSpan(sourceItem.AppName));
                row.addColumn()
                    .addContent(new TextSpan(sourceItem.Title));
                row.addColumn()
                    .addContent(new TextSpan(sourceItem.Type.DisplayText));
                listItem.setHref(this.appRedirectUrl(sourceItem.ID));
            }
        );
        if (apps.length === 0) {
            this.alert.danger('No Apps were Found');
        }
    }

    private async getApps() {
        let apps: IAppModel[];
        await this.alert.infoAction(
            'Loading...',
            async () => {
                apps = await this.hubApi.Apps.All();
            }
        );
        return apps;
    }
}