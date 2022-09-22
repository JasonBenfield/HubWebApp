import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { Command } from "@jasonbenfield/sharedwebapp/Components/Command";
import { HubAppApi } from "../../Lib/Api/HubAppApi";
import { AppListCard } from "./AppListCard";
import { AppListPanelView } from "./AppListPanelView";

interface IResult {
    appSelected?: { app: IAppModel; };
    mainMenuRequested?: {};
}

class Result {
    static appSelected(app: IAppModel) {
        return new Result({ appSelected: { app: app } });
    }

    static mainMenuRequested() {
        return new Result({ mainMenuRequested: {} });
    }

    private constructor(private readonly results: IResult) {
    }

    get appSelected() { return this.results.appSelected; }

    get mainMenuRequested() { return this.results.mainMenuRequested; }
}

export class AppListPanel implements IPanel {
    private readonly appListCard: AppListCard;
    private readonly awaitable = new Awaitable<Result>();

    constructor(
        private readonly hubApi: HubAppApi,
        private readonly view: AppListPanelView
    ) {
        this.appListCard = new AppListCard(
            this.hubApi,
            this.view.appListCard
        );
        this.appListCard.appSelected.register(this.onAppSelected.bind(this));
        new Command(this.requestMainMenu.bind(this)).add(view.menuButton);
    }

    private onAppSelected(app: IAppModel) {
        this.awaitable.resolve(
            Result.appSelected(app)
        );
    }

    private requestMainMenu() { this.awaitable.resolve(Result.mainMenuRequested()); }

    refresh() {
        return this.appListCard.refresh();
    }

    start() {
        return this.awaitable.start();
    }

    activate() { this.view.show(); }

    deactivate() { this.view.hide(); }
}