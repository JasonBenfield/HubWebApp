import { Block } from "@jasonbenfield/sharedwebapp/Html/Block";
import { FlexColumn } from "@jasonbenfield/sharedwebapp/Html/FlexColumn";
import { FlexColumnFill } from "@jasonbenfield/sharedwebapp/Html/FlexColumnFill";
import { MessageAlertView } from "@jasonbenfield/sharedwebapp/MessageAlertView";
import { PaddingCss } from "@jasonbenfield/sharedwebapp/PaddingCss";

export class StartPanelView extends Block {
    readonly alert: MessageAlertView;

    constructor() {
        super();
        this.height100();
        let flexColumn = this.addContent(new FlexColumn());
        let flexFill = flexColumn.addContent(new FlexColumnFill());
        flexFill.setPadding(PaddingCss.top(3));
        this.alert = flexFill.addContent(new MessageAlertView());
    }
}