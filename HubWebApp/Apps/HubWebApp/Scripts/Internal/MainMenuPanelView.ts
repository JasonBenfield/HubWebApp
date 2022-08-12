import { CssLengthUnit } from "@jasonbenfield/sharedwebapp/CssLengthUnit";
import { FlexCss } from "@jasonbenfield/sharedwebapp/FlexCss";
import { MarginCss } from "@jasonbenfield/sharedwebapp/MarginCss";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";
import { ButtonCommandView } from "@jasonbenfield/sharedwebapp/Views/Command";
import { GridView } from "@jasonbenfield/sharedwebapp/Views/Grid";
import { NavView } from "@jasonbenfield/sharedwebapp/Views/NavView";
import { TextLinkView } from "@jasonbenfield/sharedwebapp/Views/TextLinkView";
import { ToolbarView } from "@jasonbenfield/sharedwebapp/Views/ToolbarView";
import { HubTheme } from "./HubTheme";

export class MainMenuPanelView extends GridView {
    readonly appsLink: TextLinkView;
    readonly userGroupsLink: TextLinkView;
    readonly sessionsLink: TextLinkView;
    readonly requestsLink: TextLinkView;
    readonly logEntriesLink: TextLinkView;
    private readonly toolbar: ToolbarView;
    readonly backButton: ButtonCommandView;

    constructor(container: BasicComponentView) {
        super(container);
        this.setViewName(MainMenuPanelView.name);
        this.height100();
        this.layout();
        this.setTemplateRows(CssLengthUnit.flex(1), CssLengthUnit.auto());
        const mainContent = HubTheme.instance.mainContent(this.addCell());
        const nav = mainContent.addView(NavView);
        nav.pills();
        nav.setFlexCss(new FlexCss().column());
        this.appsLink = nav.addTextLink();
        this.appsLink.setText('Apps');
        this.appsLink.setMargin(MarginCss.bottom(3));
        this.userGroupsLink = nav.addTextLink();
        this.userGroupsLink.setText('User Groups');
        this.userGroupsLink.setMargin(MarginCss.bottom(3));
        this.sessionsLink = nav.addTextLink();
        this.sessionsLink.setText('Session Log');
        this.sessionsLink.setMargin(MarginCss.bottom(3));
        this.requestsLink = nav.addTextLink();
        this.requestsLink.setText('Access Log');
        this.requestsLink.setMargin(MarginCss.bottom(3));
        this.logEntriesLink = nav.addTextLink();
        this.logEntriesLink.setText('Event Log');
        this.logEntriesLink.setMargin(MarginCss.bottom(3));
        this.toolbar = HubTheme.instance.commandToolbar.toolbar(
            this.addCell().addView(ToolbarView)
        );
        this.backButton = HubTheme.instance.commandToolbar.backButton(
            this.toolbar.columnStart.addView(ButtonCommandView)
        );
    }

    hideToolbar() { this.toolbar.hide(); }
}