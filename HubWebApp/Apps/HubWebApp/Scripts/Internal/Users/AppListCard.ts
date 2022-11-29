import { CardAlert } from "@jasonbenfield/sharedwebapp/Components/CardAlert";
import { DefaultEvent } from "@jasonbenfield/sharedwebapp/Events";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { ListGroup } from "@jasonbenfield/sharedwebapp/Components/ListGroup";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { HubAppApi } from "../../Lib/Api/HubAppApi";
import { AppListCardView } from "../Apps/AppListCardView";
import { AppListItem } from "../Apps/AppListItem";
import { AppListItemView } from "../Apps/AppListItemView";
import { AppType } from '../../Lib/Api/AppType';

export class AppListCard {
    private readonly alert: MessageAlert;
    private readonly apps: ListGroup<AppListItem, AppListItemView>;
    private userID: number;

    private readonly _appSelected = new DefaultEvent<IAppModel>(this);
    readonly appSelected = this._appSelected.handler();

    constructor(
        private readonly hubApi: HubAppApi,
        private readonly view: AppListCardView
    ) {
        new TextComponent(this.view.titleHeader).setText('Apps');
        this.alert = new CardAlert(this.view.alert).alert;
        this.apps = new ListGroup(this.view.apps);
        this.apps.registerItemClicked(this.onAppSelected.bind(this))
    }

    setUserID(userID: number) {
        this.userID = userID;
    }

    private onAppSelected(listItem: AppListItem) {
        this._appSelected.invoke(listItem.app);
    }

    async refresh() {
        let apps = await this.getApps();
        apps = apps.filter(app => AppType.values.value(app.AppKey.Type.Value).equalsAny(AppType.values.WebApp, AppType.values.WebService));
        this.apps.setItems(
            apps,
            (app, listItem) =>
                new AppListItem(
                    app,
                    this.hubApi.AppUser.Index.getUrl(
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
            () => this.hubApi.Apps.GetApps()
        );
    }
}