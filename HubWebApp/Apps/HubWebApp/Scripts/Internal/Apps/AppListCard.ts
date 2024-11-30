import { CardAlert } from "@jasonbenfield/sharedwebapp/Components/CardAlert";
import { ListGroup } from "@jasonbenfield/sharedwebapp/Components/ListGroup";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { IMessageAlert } from "@jasonbenfield/sharedwebapp/Components/Types";
import { EventSource } from "@jasonbenfield/sharedwebapp/Events";
import { App } from "../../Lib/App";
import { HubAppClient } from "../../Lib/Http/HubAppClient";
import { AppListCardView } from "./AppListCardView";
import { AppListItem } from "./AppListItem";
import { AppListItemView } from "./AppListItemView";

type Events = { appSelected: App };

export class AppListCard {
    private readonly alert: IMessageAlert;
    private readonly apps: ListGroup<AppListItem, AppListItemView>;
    private readonly eventSource = new EventSource<Events>(this, { appSelected: null });
    readonly when = this.eventSource.when;

    constructor(
        private readonly hubClient: HubAppClient,
        private readonly view: AppListCardView
    ) {
        new TextComponent(this.view.titleHeader).setText('Apps');
        this.alert = new CardAlert(this.view.alert);
        this.apps = new ListGroup(this.view.apps);
        this.apps.when.itemClicked.then(this.onAppSelected.bind(this))
    }

    private onAppSelected(listItem: AppListItem) {
        this.eventSource.events.appSelected.invoke(listItem.app);
    }

    async refresh() {
        const sourceApps = await this.getApps();
        const apps = sourceApps.map(a => new App(a));
        this.apps.setItems(
            apps,
            (app, listItem) =>
                new AppListItem(
                    app,
                    this.hubClient.App.Index.getModifierUrl(app.getModifier(), {}).toString(),
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