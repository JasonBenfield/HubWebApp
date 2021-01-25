import { Awaitable } from "XtiShared/Awaitable";
import { Result } from "../../../../Imports/Shared/Result";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { UserListCard } from "./UserListCard";
import { UserListPanelViewModel } from "./UserListPanelViewModel";

export class UserListPanel {
    public static readonly ResultKeys = {
        userSelected: 'user-selected'
    };

    constructor(
        private readonly vm: UserListPanelViewModel,
        private readonly hubApi: HubAppApi
    ) {
        this.userListCard.userSelected.register(this.onUserSelected.bind(this));
    }

    private onUserSelected(user: IAppUserModel) {
        this.awaitable.resolve(
            new Result(UserListPanel.ResultKeys.userSelected, user)
        );
    }

    private readonly userListCard = new UserListCard(this.vm.userListCard, this.hubApi);

    refresh() {
        return this.userListCard.refresh();
    }

    private readonly awaitable = new Awaitable();

    start() {
        return this.awaitable.start();
    }
}