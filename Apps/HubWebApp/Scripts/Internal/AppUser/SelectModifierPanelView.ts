import { ButtonCommandItem } from "@jasonbenfield/sharedwebapp/Command/ButtonCommandItem";
import { Block } from "@jasonbenfield/sharedwebapp/Html/Block";
import { FlexColumn } from "@jasonbenfield/sharedwebapp/Html/FlexColumn";
import { FlexColumnFill } from "@jasonbenfield/sharedwebapp/Html/FlexColumnFill";
import { ListBlockViewModel } from "@jasonbenfield/sharedwebapp/Html/ListBlockViewModel";
import { ListGroupView } from "@jasonbenfield/sharedwebapp/ListGroup/ListGroupView";
import { MarginCss } from "@jasonbenfield/sharedwebapp/MarginCss";
import { MessageAlertView } from "@jasonbenfield/sharedwebapp/MessageAlertView";
import { HubTheme } from "../HubTheme";
import { ModifierButtonListItemView } from "./ModifierButtonListItemView";

export class SelectModifierPanelView extends Block {
    readonly alert: MessageAlertView;
    readonly modifiers: ListGroupView;
    readonly backButton: ButtonCommandItem;

    constructor() {
        super();
        this.height100();
        let flexColumn = this.addContent(new FlexColumn());
        let flexFill = flexColumn.addContent(new FlexColumnFill());
        this.alert = flexFill.addContent(new MessageAlertView());
        this.modifiers = flexFill.addContent(
            new ListGroupView(
                () => new ModifierButtonListItemView(),
                new ListBlockViewModel()
            )
        );
        this.modifiers.setMargin(MarginCss.bottom(3));
        let toolbar = flexColumn.addContent(HubTheme.instance.commandToolbar.toolbar());
        this.backButton = toolbar.columnStart.addContent(
            HubTheme.instance.commandToolbar.backButton()
        );
    }
}