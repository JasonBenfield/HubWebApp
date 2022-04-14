import { ButtonCommandItem } from "@jasonbenfield/sharedwebapp/Command/ButtonCommandItem";
import { Block } from "@jasonbenfield/sharedwebapp/Html/Block";
import { FlexColumn } from "@jasonbenfield/sharedwebapp/Html/FlexColumn";
import { FlexColumnFill } from "@jasonbenfield/sharedwebapp/Html/FlexColumnFill";
import { MarginCss } from "@jasonbenfield/sharedwebapp/MarginCss";
import { AppListCardView } from "../../Apps/AppListCardView";
import { HubTheme } from "../../HubTheme";
import { UserComponentView } from "./UserComponentView";

export class UserPanelView extends Block {
    readonly userComponent: UserComponentView;
    readonly appListCard: AppListCardView;
    readonly backButton: ButtonCommandItem;

    constructor() {
        super();
        this.height100();
        this.setName(UserPanelView.name);
        let flexColumn = this.addContent(new FlexColumn());
        let flexFill = flexColumn.addContent(new FlexColumnFill());
        this.userComponent = flexFill.container.addContent(new UserComponentView())
            .configure(c => c.setMargin(MarginCss.bottom(3)));
        this.appListCard = flexFill.container.addContent(new AppListCardView());
        this.appListCard.setMargin(MarginCss.bottom(3));
        let toolbar = flexColumn.addContent(HubTheme.instance.commandToolbar.toolbar());
        this.backButton = toolbar.columnStart.addContent(HubTheme.instance.commandToolbar.backButton());
        this.backButton.setText('App Permissions');
    }
}