﻿import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { Command } from "@jasonbenfield/sharedwebapp/Components/Command";
import { WebPage } from "@jasonbenfield/sharedwebapp/Http/WebPage";
import { HubAppClient } from "../../Lib/Http/HubAppClient";
import { AppListCard } from "./AppListCard";
import { UserComponent } from "./UserComponent";
import { UserPanelView } from "./UserPanelView";
import { UserAuthenticatorListCard } from "./UserAuthenticatorListCard";
import { App } from "../../Lib/App";

interface IResults {
    backRequested?: boolean;
    editRequested?: { userID: number; };
    changePasswordRequested?: { userID: number };
}

class Result {
    static backRequested() { return new Result({ backRequested: true }); }

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
    private readonly userAuthenticatorListCard: UserAuthenticatorListCard;
    private readonly appListCard: AppListCard;
    private userID: number;
    private readonly awaitable = new Awaitable<Result>();
    private readonly backCommand = new Command(this.back.bind(this));

    constructor(
        private readonly hubClient: HubAppClient,
        private readonly view: UserPanelView
    ) {
        this.userComponent = new UserComponent(hubClient, view.userComponent);
        this.appListCard = new AppListCard(
            this.hubClient,
            this.view.appListCard
        );
        this.backCommand.add(view.backButton);
        this.userAuthenticatorListCard = new UserAuthenticatorListCard(hubClient, view.userAuthenticatorListCard);
        this.userComponent.when.editRequested.then(this.onEditRequested.bind(this));
        this.userComponent.when.changePasswordRequested.then(this.onChangePasswordRequested.bind(this));
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
        this.userComponent.setUserID(userID);
        this.userAuthenticatorListCard.setUserID(userID);
        this.appListCard.setUserID(userID);
    }

    refresh() {
        const promises: Promise<any>[] = [
            this.userComponent.refresh(),
            this.userAuthenticatorListCard.refresh(),
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