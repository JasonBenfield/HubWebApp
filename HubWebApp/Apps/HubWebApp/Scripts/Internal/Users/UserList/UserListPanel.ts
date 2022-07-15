import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { HubAppApi } from "../../../Lib/Api/HubAppApi";
import { UserListCard } from "./UserListCard";
import { UserListPanelView } from "./UserListPanelView";

interface IResult {
    userSelected?: { user: IAppUserModel};
}

class Result {
    static userSelected(user: IAppUserModel) {
        return new Result({ userSelected: { user: user} });
    }

    private constructor(private readonly results: IResult) { }

    get userSelected() { return this.results.userSelected; }
}

export class UserListPanel implements IPanel {
    private readonly userListCard: UserListCard;
    private readonly awaitable = new Awaitable<Result>();

    constructor(
        private readonly hubApi: HubAppApi,
        private readonly view: UserListPanelView
    ) {
        this.userListCard = new UserListCard(this.hubApi, this.view.userListCard);
        this.userListCard.userSelected.register(this.onUserSelected.bind(this));
    }

    private onUserSelected(user: IAppUserModel) {
        this.awaitable.resolve(
            Result.userSelected(user)
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