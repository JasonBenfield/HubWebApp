import { Block } from "@jasonbenfield/sharedwebapp/Html/Block";
import { FlexColumn } from "@jasonbenfield/sharedwebapp/Html/FlexColumn";
import { FlexColumnFill } from "@jasonbenfield/sharedwebapp/Html/FlexColumnFill";
import { MessageAlertView } from "@jasonbenfield/sharedwebapp/MessageAlertView";

export class AppUserDataPanelView extends Block {
    readonly alert: MessageAlertView;

    constructor() {
        super();
        this.height100();
        let flexColumn = this.addContent(new FlexColumn());
        let flexFill = flexColumn.addContent(new FlexColumnFill());
        this.alert = flexFill.addContent(new MessageAlertView());
    }
}