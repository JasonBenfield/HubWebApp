import { CssLengthUnit } from "../../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/CssLengthUnit";
import { BasicComponentView } from "../../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Views/BasicComponentView";
import { GridView } from "../../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Views/Grid";
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