import { CardAlert } from "@jasonbenfield/sharedwebapp/Components/CardAlert";
import { ListGroup } from "@jasonbenfield/sharedwebapp/Components/ListGroup";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { IMessageAlert } from "@jasonbenfield/sharedwebapp/Components/Types";
import { EventSource } from "@jasonbenfield/sharedwebapp/Events";
import { App } from "../../Lib/App";
import { AppType } from '../../Lib/Http/AppType';
import { HubAppClient } from "../../Lib/Http/HubAppClient";
import { AppListCardView } from "../Apps/AppListCardView";
import { AppListItem } from "../Apps/AppListItem";
import { AppListItemView } from "../Apps/AppListItemView";

type Events = { appSelected: App };

export class AppListCard {
    private readonly alert: IMessageAlert;
    private readonly apps: ListGroup<AppListItem, AppListItemView>;
    private readonly eventSource = new EventSource<Events>(this, { appSelected: null });
    readonly when = this.eventSource.when;
    private userID: number;

    constructor(
        private readonly hubClient: HubAppClient,
        private readonly view: AppListCardView
    ) {
        new TextComponent(this.view.titleHeader).setText('Apps');
        this.alert = new CardAlert(this.view.alert);
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
        const apps = await this.getApps();
        const webApps = apps.filter(app => app.appKey.type.equals(AppType.values.WebApp));
        this.apps.setItems(
            webApps,
            (app, listItem) =>
                new AppListItem(
                    app,
                    this.hubClient.AppUser.Index.getUrl(
                        { App: app.publicKey.displayText, UserID: this.userID }
                    ).toString(),
                    listItem
                )
        );
        if (webApps.length === 0) {
            this.alert.danger('No Web Apps were Found');
        }
    }

    private async getApps() {
        const sourceApps = await this.alert.infoAction(
            "Loading...",
            () => this.hubClient.Apps.GetApps()
        );
        return sourceApps.map(a => new App(a));
    }
}