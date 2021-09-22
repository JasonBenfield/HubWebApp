import { Awaitable } from "XtiShared/Awaitable";
import { Block } from "XtiShared/Html/Block";
import { BlockViewModel } from "XtiShared/Html/BlockViewModel";
import { FlexColumn } from "XtiShared/Html/FlexColumn";
import { FlexColumnFill } from "XtiShared/Html/FlexColumnFill";
import { Result } from "XtiShared/Result";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { UserListCard } from "./UserListCard";

export class UserListPanel extends Block {
    public static readonly ResultKeys = {
        userSelected: 'user-selected'
    };

    constructor(
        private readonly hubApi: HubAppApi,
        vm: BlockViewModel = new BlockViewModel()
    ) {
        super(vm);
        this.height100();
        this.setName(UserListPanel.name);
        let flexColumn = this.addContent(new FlexColumn());
        let flexFill = flexColumn.addContent(new FlexColumnFill());
        this.userListCard = flexFill.container.addContent(new UserListCard(this.hubApi));
        this.userListCard.userSelected.register(this.onUserSelected.bind(this));
    }

    private onUserSelected(user: IAppUserModel) {
        this.awaitable.resolve(
            new Result(UserListPanel.ResultKeys.userSelected, user)
        );
    }

    private readonly userListCard: UserListCard;

    refresh() {
        return this.userListCard.refresh();
    }

    private readonly awaitable = new Awaitable();

    start() {
        return this.awaitable.start();
    }
}