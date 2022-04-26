import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { HubAppApi } from "../../Hub/Api/HubAppApi";
import { AppListCard } from "./AppListCard";
import { AppListPanelView } from "./AppListPanelView";

interface Results {
    appSelected?: { app: IAppModel; };
}

export class AppListPanelResult {
    static appSelected(app: IAppModel) {
        return new AppListPanelResult({ appSelected: { app: app } });
    }

    private constructor(private readonly results: Results) {
    }

    get appSelected() { return this.results.appSelected; }
}

export class AppListPanel {
    public static readonly ResultKeys = {
    }

    private readonly appListCard: AppListCard;
    private readonly awaitable = new Awaitable<AppListPanelResult>();

    constructor(
        private readonly hubApi: HubAppApi,
        private readonly view: AppListPanelView
    ) {
        this.appListCard = new AppListCard(
            this.hubApi,
            modKey => this.hubApi.App.Index.getModifierUrl(modKey, {}).toString(),
            this.view.appListCard
        );
        this.appListCard.appSelected.register(this.onAppSelected.bind(this));
    }

    private onAppSelected(app: IAppModel) {
        this.awaitable.resolve(
            AppListPanelResult.appSelected(app)
        );
    }

    refresh() {
        return this.appListCard.refresh();
    }

    start() {
        return this.awaitable.start();
    }


}