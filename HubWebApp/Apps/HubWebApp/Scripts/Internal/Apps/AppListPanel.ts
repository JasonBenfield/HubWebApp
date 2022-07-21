import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { HubAppApi } from "../../Lib/Api/HubAppApi";
import { AppListCard } from "./AppListCard";
import { AppListPanelView } from "./AppListPanelView";

interface IResult {
    appSelected?: { app: IAppModel; };
}

class Result {
    static appSelected(app: IAppModel) {
        return new Result({ appSelected: { app: app } });
    }

    private constructor(private readonly results: IResult) {
    }

    get appSelected() { return this.results.appSelected; }
}

export class AppListPanel {
    private readonly appListCard: AppListCard;
    private readonly awaitable = new Awaitable<Result>();

    constructor(
        private readonly hubApi: HubAppApi,
        private readonly view: AppListPanelView
    ) {
        this.appListCard = new AppListCard(
            this.hubApi,
            app => this.hubApi.App.Index.getModifierUrl(app.PublicKey.DisplayText, {}).toString(),
            this.view.appListCard
        );
        this.appListCard.appSelected.register(this.onAppSelected.bind(this));
    }

    private onAppSelected(app: IAppModel) {
        this.awaitable.resolve(
            Result.appSelected(app)
        );
    }

    refresh() {
        return this.appListCard.refresh();
    }

    start() {
        return this.awaitable.start();
    }


}