import { CardAlert } from "@jasonbenfield/sharedwebapp/Components/CardAlert";
import { DefaultEvent } from "@jasonbenfield/sharedwebapp/Events";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { ListGroup } from "@jasonbenfield/sharedwebapp/Components/ListGroup";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { HubAppApi } from "../../Lib/Api/HubAppApi";
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
        new TextComponent(this.view.titleHeader).setText('Apps');
        this.alert = new CardAlert(this.view.alert).alert;
        this.apps = new ListGroup(this.view.apps);
        this.apps.registerItemClicked(this.onAppSelected.bind(this))
    }

    private onAppSelected(listItem: AppListItem) {
        this._appSelected.invoke(listItem.app);
    }

    async refresh() {
        const apps = await this.getApps();
        this.apps.setItems(
            apps,
            (sourceItem: IAppModel, listItem: AppListItemView) =>
                new AppListItem(sourceItem, this.appRedirectUrl, listItem)
        );
        if (apps.length === 0) {
            this.alert.danger('No Apps were Found');
        }
    }

    private getApps() {
        return this.alert.infoAction(
            'Loading...',
            () => this.hubApi.Apps.GetApps()
        );
    }
}