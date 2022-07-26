import { CssLengthUnit } from "../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/CssLengthUnit";
import { MarginCss } from "../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/MarginCss";
import { BasicComponentView } from "../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Views/BasicComponentView";
import { ButtonCommandView } from "../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Views/Command";
import { GridView } from "../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Views/Grid";
import { NavView } from "../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Views/NavView";
import { TextLinkView } from "../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Views/TextLinkView";
import { ToolbarView } from "../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Views/ToolbarView";
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