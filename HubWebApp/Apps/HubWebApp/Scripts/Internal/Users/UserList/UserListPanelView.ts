import { Block } from "@jasonbenfield/sharedwebapp/Html/Block";
import { FlexColumn } from "@jasonbenfield/sharedwebapp/Html/FlexColumn";
import { FlexColumnFill } from "@jasonbenfield/sharedwebapp/Html/FlexColumnFill";
import { UserListCardView } from "./UserListCardView";

export class UserListPanelView extends Block {
    readonly userListCard: UserListCardView;

    constructor() {
        super();
        this.height100();
        this.setName(UserListPanelView.name);
        let flexColumn = this.addContent(new FlexColumn());
        let flexFill = flexColumn.addContent(new FlexColumnFill());
        this.userListCard = flexFill.container.addContent(new UserListCardView());
    }
}