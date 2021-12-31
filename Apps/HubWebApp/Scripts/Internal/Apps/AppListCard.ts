import { DefaultEvent } from "@jasonbenfield/sharedwebapp/Events";
import { TextBlock } from "@jasonbenfield/sharedwebapp/Html/TextBlock";
import { ListGroup } from "@jasonbenfield/sharedwebapp/ListGroup/ListGroup";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/MessageAlert";
import { HubAppApi } from "../../Hub/Api/HubAppApi";
import { AppListCardView } from "./AppListCardView";
import { AppListItem } from "./AppListItem";
import { AppListItemView } from "./AppListItemView";

export class AppListCard {
    private readonly alert: MessageAlert;
    private readonly apps: ListGroup;

    private readonly _appSelected = new DefaultEvent<IAppModel>(this);
    readonly appSelected = this._appSelected.handler();

    constructor(
        private readonly hubApi: HubAppApi,
        private readonly appRedirectUrl: (appID: number) => string,
        private readonly view: AppListCardView
    ) {
        new TextBlock('Apps', this.view.titleHeader);
        this.alert = new MessageAlert(this.view.alert);
        this.apps = new ListGroup(this.view.apps);
        this.apps.itemClicked.register(this.onAppSelected.bind(this))
    }

    private onAppSelected(listItem: AppListItem) {
        this._appSelected.invoke(listItem.app);
    }

    async refresh() {
        let apps = await this.getApps();
        this.apps.setItems(
            apps,
            (sourceItem: IAppModel, listItem: AppListItemView) =>
                new AppListItem(sourceItem, this.appRedirectUrl, listItem)
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