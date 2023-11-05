import { CardAlert } from "@jasonbenfield/sharedwebapp/Components/CardAlert";
import { DefaultEvent } from "@jasonbenfield/sharedwebapp/Events";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { ListGroup } from "@jasonbenfield/sharedwebapp/Components/ListGroup";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { HubAppClient } from "../../Lib/Http/HubAppClient";
import { AppListCardView } from "./AppListCardView";
import { AppListItem } from "./AppListItem";
import { AppListItemView } from "./AppListItemView";

export class AppListCard {
    private readonly alert: MessageAlert;
    private readonly apps: ListGroup<AppListItem, AppListItemView>;

    private readonly _appSelected = new DefaultEvent<IAppModel>(this);
    readonly appSelected = this._appSelected.handler();

    constructor(
        private readonly hubClient: HubAppClient,
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
            (app, listItem) =>
                new AppListItem(
                    app,
                    this.hubClient.App.Index.getModifierUrl(app.PublicKey.DisplayText, {}).toString(),
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