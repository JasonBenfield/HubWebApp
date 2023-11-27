import { CardAlert } from "@jasonbenfield/sharedwebapp/Components/CardAlert";
import { EventSource } from "@jasonbenfield/sharedwebapp/Events";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { ListGroup } from "@jasonbenfield/sharedwebapp/Components/ListGroup";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { HubAppClient } from "../../Lib/Http/HubAppClient";
import { AppListCardView } from "../Apps/AppListCardView";
import { AppListItem } from "../Apps/AppListItem";
import { AppListItemView } from "../Apps/AppListItemView";
import { AppType } from '../../Lib/Http/AppType';
import { App } from "../../Lib/App";

type Events = { appSelected: App };

export class AppListCard {
    private readonly alert: MessageAlert;
    private readonly apps: ListGroup<AppListItem, AppListItemView>;
    private readonly eventSource = new EventSource<Events>(this, { appSelected: null });
    readonly when = this.eventSource.when;
    private userID: number;

    constructor(
        private readonly hubClient: HubAppClient,
        private readonly view: AppListCardView
    ) {
        new TextComponent(this.view.titleHeader).setText('Apps');
        this.alert = new CardAlert(this.view.alert).alert;
        this.apps = new ListGroup(this.view.apps);
        this.apps.when.itemClicked.then(this.onAppSelected.bind(this))
    }

    setUserID(userID: number) {
        this.userID = userID;
    }

    private onAppSelected(listItem: AppListItem) {
        this.eventSource.events.appSelected.invoke(listItem.app);
    }

    async refresh() {
        let apps = await this.getApps();
        apps = apps.filter(app => AppType.values.value(app.AppKey.Type.Value).equals(AppType.values.WebApp));
        this.apps.setItems(
            apps,
            (app, listItem) =>
                new AppListItem(
                    new App(app),
                    this.hubClient.AppUser.Index.getUrl(
                        { App: app.PublicKey.DisplayText, UserID: this.userID }
                    ).toString(),
                    listItem
                )
        );
        if (apps.length === 0) {
            this.alert.danger('No Apps were Found');
        }
    }

    private getApps() {
        return this.alert.infoAction(
            'Loading...',
            () => this.hubClient.Apps.GetApps()
        );
    }
}