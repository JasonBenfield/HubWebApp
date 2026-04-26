import { CssLengthUnit } from "@jasonbenfield/sharedwebapp/CssLengthUnit";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";
import { GridView } from "@jasonbenfield/sharedwebapp/Views/Grid";
import { MessageAlertView } from "@jasonbenfield/sharedwebapp/Views/MessageAlertView";
import { HubTheme } from "../HubTheme";
import { CommandCardView } from "./CommandCardView";
import { MarginCss } from "@jasonbenfield/sharedwebapp/MarginCss";
import { ButtonCommandView, LinkCommandView } from "@jasonbenfield/sharedwebapp/Views/Command";
import { ToolbarView } from "@jasonbenfield/sharedwebapp/Views/ToolbarView";
import { CommandStepListCardView } from "./CommandStepListCardView";
import { ButtonContainerView } from "@jasonbenfield/sharedwebapp/Views/ButtonContainerView";

export class CommandPanelView extends GridView {
    readonly alertView: MessageAlertView;
    readonly commandCardView: CommandCardView;
    readonly viewAppButton: LinkCommandView;
    readonly viewCurrentInstallationButton: LinkCommandView;
    readonly viewVersionInstallationButton: LinkCommandView;
    readonly viewInstallationButton: LinkCommandView;
    readonly stepListCardView: CommandStepListCardView;
    readonly menuButton: ButtonCommandView;
    readonly refreshButton: ButtonCommandView;

    constructor(container: BasicComponentView) {
        super(container);
        this.styleAsLayout();
        this.height100();
        this.setTemplateRows(CssLengthUnit.flex(1), CssLengthUnit.auto());
        const mainContent = HubTheme.instance.mainContent(this.addCell());
        this.alertView = mainContent.addView(MessageAlertView);
        this.commandCardView = mainContent.addView(CommandCardView);
        this.commandCardView.setMargin(MarginCss.bottom(3));
        const buttonContainerView = mainContent.addView(ButtonContainerView);
        this.viewAppButton = buttonContainerView.addLinkCommand();
        this.viewCurrentInstallationButton = buttonContainerView.addLinkCommand();
        this.viewVersionInstallationButton = buttonContainerView.addLinkCommand();
        this.viewInstallationButton = buttonContainerView.addLinkCommand();
        this.stepListCardView = mainContent.addView(CommandStepListCardView);
        this.stepListCardView.setMargin(MarginCss.bottom(3));
        const toolbar = HubTheme.instance.commandToolbar.toolbar(this.addCell().addView(ToolbarView));
        this.menuButton = HubTheme.instance.commandToolbar.menuButton(toolbar.addButtonCommandToStart());
        this.menuButton.setMargin(MarginCss.end(1));
        this.refreshButton = HubTheme.instance.commandToolbar.refreshButton(toolbar.addButtonCommandToStart());
    }
}