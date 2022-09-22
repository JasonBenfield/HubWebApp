import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { Command } from "@jasonbenfield/sharedwebapp/Components/Command";
import { WebPage } from "@jasonbenfield/sharedwebapp/Api/WebPage";
import { HubAppApi } from "../../Lib/Api/HubAppApi";
import { AppListCard } from "./AppListCard";
import { UserComponent } from "./UserComponent";
import { UserPanelView } from "./UserPanelView";

interface IResults {
    backRequested?: {};
    editRequested?: { userID: number; };
    changePasswordRequested?: { userID: number };
}

class Result {
    static backRequested() { return new Result({ backRequested: {} }); }

    static editRequested(userID: number) {
        return new Result({ editRequested: { userID: userID } });
    }

    static changePasswordRequested(userID: number) {
        return new Result({ changePasswordRequested: { userID: userID } });
    }

    private constructor(private readonly results: IResults) { }

    get backRequested() { return this.results.backRequested; }

    get editRequested() { return this.results.editRequested; }

    get changePasswordRequested() { return this.results.changePasswordRequested; }
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
            this.view.appListCard
        );
        this.backCommand.add(this.view.backButton);
        this.appListCard.appSelected.register(this.onAppSelected.bind(this));
        this.userComponent.when.editRequested.then(this.onEditRequested.bind(this));
        this.userComponent.when.changePasswordRequested.then(this.onChangePasswordRequested.bind(this));
    }

    private onAppSelected(app: IAppModel) {
        const url = this.hubApi.AppUser.Index.getModifierUrl(
            app.PublicKey.DisplayText,
            { App: app.PublicKey.DisplayText, UserID: this.userID }
        );
        new WebPage(url).open();
    }

    private onEditRequested(userID: number) {
        this.awaitable.resolve(
            Result.editRequested(userID)
        );
    }

    private onChangePasswordRequested(userID: number) {
        this.awaitable.resolve(
            Result.changePasswordRequested(userID)
        );
    }

    setUserID(userID: number) {
        this.userID = userID;
        this.appListCard.setUserID(userID);
        this.userComponent.setUserID(userID);
    }

    refresh() {
        const promises: Promise<any>[] = [
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