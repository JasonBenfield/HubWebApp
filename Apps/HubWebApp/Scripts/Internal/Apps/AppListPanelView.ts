import { Block } from "@jasonbenfield/sharedwebapp/Html/Block";
import { FlexColumn } from "@jasonbenfield/sharedwebapp/Html/FlexColumn";
import { FlexColumnFill } from "@jasonbenfield/sharedwebapp/Html/FlexColumnFill";
import { AppListCardView } from "./AppListCardView";

export class AppListPanelView extends Block {
    readonly appListCard: AppListCardView;

    constructor() {
        super();
        this.height100();
        let flexColumn = this.addContent(new FlexColumn());
        this.appListCard = flexColumn.addContent(new FlexColumnFill())
            .addContent(new AppListCardView());
    }
}