import { CssLengthUnit } from "@jasonbenfield/sharedwebapp/CssLengthUnit";
import { FlexCss } from "@jasonbenfield/sharedwebapp/FlexCss";
import { MarginCss } from "@jasonbenfield/sharedwebapp/MarginCss";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";
import { ButtonCommandView, LinkCommandView } from "@jasonbenfield/sharedwebapp/Views/Command";
import { GridView } from "@jasonbenfield/sharedwebapp/Views/Grid";
import { NavView } from "@jasonbenfield/sharedwebapp/Views/NavView";
import { ToolbarView } from "@jasonbenfield/sharedwebapp/Views/ToolbarView";
import { HubTheme } from "./HubTheme";
import { ButtonContainerView } from "@jasonbenfield/sharedwebapp/Views/ButtonContainerView";
import { MessageAlertView } from "@jasonbenfield/sharedwebapp/Views/MessageAlertView";

export class MainMenuPanelView extends GridView {
    readonly alertView: MessageAlertView;
    readonly appsButton: LinkCommandView;
    readonly userGroupsButton: LinkCommandView;
    readonly userRolesButton: LinkCommandView;
    readonly sessionLogButton: LinkCommandView;
    readonly accessLogButton: LinkCommandView;
    readonly eventLogButton: LinkCommandView;
    readonly installationsButton: LinkCommandView;
    readonly backButton: ButtonCommandView;
    private readonly toolbar: ToolbarView;

    constructor(container: BasicComponentView) {
        super(container);
        this.setViewName(MainMenuPanelView.name);
        this.height100();
        this.styleAsLayout();
        this.setTemplateRows(CssLengthUnit.flex(1), CssLengthUnit.auto());
        const mainContent = HubTheme.instance.mainContent(this.addCell());
        this.alertView = mainContent.addView(MessageAlertView);
        const buttonContainer = mainContent.addView(ButtonContainerView);
        this.appsButton = buttonContainer.addLinkCommand();
        this.appsButton.setText("Apps");
        this.userGroupsButton = buttonContainer.addLinkCommand();
        this.userGroupsButton.setText("User Groups");
        this.userRolesButton = buttonContainer.addLinkCommand();
        this.userRolesButton.setText("User Roles");
        this.sessionLogButton = buttonContainer.addLinkCommand();
        this.sessionLogButton.setText("Session Log");
        this.accessLogButton = buttonContainer.addLinkCommand();
        this.accessLogButton.setText("Access Log");
        this.eventLogButton = buttonContainer.addLinkCommand();
        this.eventLogButton.setText("Event Log");
        this.installationsButton = buttonContainer.addLinkCommand();
        this.installationsButton.setText("Installations");
        const menu = mainContent.addView(NavView);
        menu.pills();
        menu.setFlexCss(new FlexCss().column());
        menu.configListItem(li => li.setMargin(MarginCss.bottom(3)));
        this.toolbar = HubTheme.instance.commandToolbar.toolbar(
            this.addCell().addView(ToolbarView)
        );
        this.backButton = HubTheme.instance.commandToolbar.backButton(
            this.toolbar.columnStart.addView(ButtonCommandView)
        );
    }

    hideToolbar() { this.toolbar.hide(); }
}