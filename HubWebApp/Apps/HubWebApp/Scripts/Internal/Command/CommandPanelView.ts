import { CssLengthUnit } from "@jasonbenfield/sharedwebapp/CssLengthUnit";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";
import { GridView } from "@jasonbenfield/sharedwebapp/Views/Grid";
import { MessageAlertView } from "@jasonbenfield/sharedwebapp/Views/MessageAlertView";
import { HubTheme } from "../HubTheme";
import { CommandCardView } from "./CommandCardView";
import { MarginCss } from "@jasonbenfield/sharedwebapp/MarginCss";
import { ButtonCommandView } from "@jasonbenfield/sharedwebapp/Views/Command";
import { ToolbarView } from "@jasonbenfield/sharedwebapp/Views/ToolbarView";

export class CommandPanelView extends GridView {
    readonly alertView: MessageAlertView;
    readonly commandCardView: CommandCardView;
    readonly menuButton: ButtonCommandView;

    constructor(container: BasicComponentView) {
        super(container);
        this.styleAsLayout();
        this.height100();
        this.setTemplateRows(CssLengthUnit.flex(1), CssLengthUnit.auto());
        const mainContent = HubTheme.instance.mainContent(this.addCell());
        this.alertView = mainContent.addView(MessageAlertView);
        this.commandCardView = mainContent.addView(CommandCardView);
        this.commandCardView.setMargin(MarginCss.bottom(3));
        const toolbar = HubTheme.instance.commandToolbar.toolbar(this.addCell().addView(ToolbarView));
        this.menuButton = HubTheme.instance.commandToolbar.menuButton(toolbar.addButtonCommandToStart());
    }
}