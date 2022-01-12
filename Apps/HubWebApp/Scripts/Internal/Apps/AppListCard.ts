import { CardAlert } from "@jasonbenfield/sharedwebapp/Card/CardAlert";
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
        private readonly appRedirectUrl: (modKey: string) => string,
        private readonly view: AppListCardView
    ) {
        new TextBlock('Apps', this.view.titleHeader);
        this.alert = new CardAlert(this.view.alert).alert;
        this.apps = new ListGroup(this.view.apps);
        this.apps.itemClicked.register(this.onAppSelected.bind(this))
    }

    private onAppSelected(listItem: AppListItem) {
        this._appSelected.invoke(listItem.appWithModKey.App);
    }

    async refresh() {
        let apps = await this.getApps();
        this.apps.setItems(
            apps,
            (sourceItem: IAppWithModKeyModel, listItem: AppListItemView) =>
                new AppListItem(sourceItem, this.appRedirectUrl, listItem)
        );
        if (apps.length === 0) {
            this.alert.danger('No Apps were Found');
        }
    }

    private getApps() {
        return this.alert.infoAction(
            'Loading...',
            () =>  this.hubApi.Apps.GetApps()
        );
    }
}