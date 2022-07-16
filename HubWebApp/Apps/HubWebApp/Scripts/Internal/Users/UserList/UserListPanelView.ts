import { CssLengthUnit } from "@jasonbenfield/sharedwebapp/CssLengthUnit";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";
import { GridView } from "@jasonbenfield/sharedwebapp/Views/Grid";
import { HubTheme } from "../../HubTheme";
import { UserListCardView } from "./UserListCardView";

export class UserListPanelView extends GridView {
    readonly userListCard: UserListCardView;

    constructor(container: BasicComponentView) {
        super(container);
        this.setViewName(UserListPanelView.name);
        this.height100();
        this.layout();
        this.setTemplateRows(CssLengthUnit.flex(1));
        const mainContent = HubTheme.instance.mainContent(this.addCell());
        this.userListCard = mainContent.addView(UserListCardView);
    }
}