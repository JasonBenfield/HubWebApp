import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { Command } from "@jasonbenfield/sharedwebapp/Components/Command";
import { HubAppApi } from "../../../Lib/Api/HubAppApi";
import { AppListCard } from "../../Apps/AppListCard";
import { UserComponent } from "./UserComponent";
import { UserPanelView } from "./UserPanelView";

interface IResults {
    backRequested?: {};
    appSelected?: { app: IAppModel; };
    editRequested?: { userID: number;};
}

class Result {
    static backRequested() { return new Result({ backRequested: {} }); }

    static appSelected(app: IAppModel) {
        return new Result({ appSelected: { app: app } });
    }

    static editRequested(userID: number) {
        return new Result({ editRequested: { userID: userID } });
    }

    private constructor(private readonly results: IResults) { }

    get backRequested() { return this.results.backRequested; }

    get appSelected() { return this.results.appSelected; }

    get editRequested() { return this.results.editRequested; }
}

export class UserPanel implements IPanel {
    private readonly userComponent: UserComponent;
    private readonly appListCard: AppListCard;
    private userID: number;
    private readonly awaitable = new Awaitable<Result>();
    private readonly backCommand = new Command(this.back.bind(this));

    constructor(
        private readonly hubApi: HubAppApi,
        private readonly view: UserPanelView
    ) {
        this.userComponent = new UserComponent(this.hubApi, this.view.userComponent);
        this.appListCard = new AppListCard(
            this.hubApi,
            modKey => this.hubApi.AppUser.Index.getModifierUrl(modKey, this.userID).toString(),
            this.view.appListCard
        );
        this.backCommand.add(this.view.backButton);
        this.appListCard.appSelected.register(this.onAppSelected.bind(this));
        this.userComponent.editRequested.register(this.onEditRequested.bind(this));
    }

    private onAppSelected(app: IAppModel) {
        this.awaitable.resolve(
            Result.appSelected(app)
        );
    }

    private onEditRequested(userID: number) {
        this.awaitable.resolve(
            Result.editRequested(userID)
        );
    }

    setUserID(userID: number) {
        this.userID = userID;
        this.userComponent.setUserID(userID);
    }

    refresh() {
        let promises: Promise<any>[] = [
            this.userComponent.refresh(),
            this.appListCard.refresh()
        ];
        return Promise.all(promises);
    }

    start() {
        return this.awaitable.start();
    }

    private back() {
        this.awaitable.resolve(Result.backRequested());
    }

    activate() { this.view.show(); }

    deactivate() { this.view.hide(); }
}