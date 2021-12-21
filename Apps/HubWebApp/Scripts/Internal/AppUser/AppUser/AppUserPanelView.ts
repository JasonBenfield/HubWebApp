import { ButtonCommandItem } from "@jasonbenfield/sharedwebapp/Command/ButtonCommandItem";
import { Block } from "@jasonbenfield/sharedwebapp/Html/Block";
import { FlexColumn } from "@jasonbenfield/sharedwebapp/Html/FlexColumn";
import { FlexColumnFill } from "@jasonbenfield/sharedwebapp/Html/FlexColumnFill";
import { MarginCss } from "@jasonbenfield/sharedwebapp/MarginCss";
import { HubTheme } from "../../HubTheme";
import { UserComponentView } from "./UserComponentView";
import { UserModCategoryListCardView } from "./UserModCategoryListCardView";
import { UserRoleListCardView } from "./UserRoleListCardView";

export class AppUserPanelView extends Block {
    readonly userComponent: UserComponentView;
    readonly userRoles: UserRoleListCardView;
    readonly userModCategories: UserModCategoryListCardView;
    readonly backButton: ButtonCommandItem;

    constructor() {
        super();
        this.height100();
        let flexColumn = this.addContent(new FlexColumn());
        let flexFill = flexColumn.addContent(new FlexColumnFill());
        this.userComponent = flexFill.addContent(new UserComponentView())
            .configure(c => c.setMargin(MarginCss.bottom(3)));
        this.userRoles = flexFill.addContent(new UserRoleListCardView())
            .configure(c => c.setMargin(MarginCss.bottom(3)));
        this.userModCategories = flexFill.addContent(new UserModCategoryListCardView());
        let toolbar = flexColumn.addContent(HubTheme.instance.commandToolbar.toolbar());
        this.backButton = toolbar.columnStart.addContent(
            HubTheme.instance.commandToolbar.backButton()
        );
        this.backButton.setText('User');
    }
}