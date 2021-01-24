import { HubAppApi } from "../../Hub/Api/HubAppApi";
import { UserListCard } from "./UserListCard";
import { UserListPanelViewModel } from "./UserListPanelViewModel";

export class UserListPanel {
    constructor(
        private readonly vm: UserListPanelViewModel,
        private readonly hubApi: HubAppApi
    ) {
    }

    private readonly userListCard = new UserListCard(this.vm.userListCard, this.hubApi);

    refresh() {
        return this.userListCard.refresh();
    }
}