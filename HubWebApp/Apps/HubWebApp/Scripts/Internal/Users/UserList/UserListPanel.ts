import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { UserListCard } from "./UserListCard";
import { UserListPanelView } from "./UserListPanelView";

interface Results {
    userSelected?: { user: IAppUserModel};
}

export class UserListPanelResult {
    static userSelected(user: IAppUserModel) {
        return new UserListPanelResult({ userSelected: { user: user} });
    }

    private constructor(private readonly results: Results) { }

    get userSelected() { return this.results.userSelected; }
}

export class UserListPanel implements IPanel {
    private readonly userListCard: UserListCard;
    private readonly awaitable = new Awaitable<UserListPanelResult>();

    constructor(
        private readonly hubApi: HubAppApi,
        private readonly view: UserListPanelView
    ) {
        this.userListCard = new UserListCard(this.hubApi, this.view.userListCard);
        this.userListCard.userSelected.register(this.onUserSelected.bind(this));
    }

    private onUserSelected(user: IAppUserModel) {
        this.awaitable.resolve(
            UserListPanelResult.userSelected(user)
        );
    }

    refresh() {
        return this.userListCard.refresh();
    }

    start() {
        return this.awaitable.start();
    }

    activate() { this.view.show(); }

    deactivate() { this.view.hide(); }
}