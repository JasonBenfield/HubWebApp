import { CssLengthUnit } from "@jasonbenfield/sharedwebapp/CssLengthUnit";
import { PaddingCss } from "@jasonbenfield/sharedwebapp/PaddingCss";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";
import { BlockView } from "@jasonbenfield/sharedwebapp/Views/BlockView";
import { ButtonCommandView } from "@jasonbenfield/sharedwebapp/Views/Command";
import { GridView } from "@jasonbenfield/sharedwebapp/Views/Grid";
import { LinkListGroupView, TextLinkListGroupItemView } from "@jasonbenfield/sharedwebapp/Views/ListGroup";
import { MessageAlertView } from "@jasonbenfield/sharedwebapp/Views/MessageAlertView";
import { ToolbarView } from "@jasonbenfield/sharedwebapp/Views/ToolbarView";
import { MarginCss } from "../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/MarginCss";
import { HubTheme } from "../HubTheme";

export class UserGroupsPanelView extends GridView {
    readonly alert: MessageAlertView;
    readonly userGroups: LinkListGroupView;
    readonly menuButton: ButtonCommandView;
    readonly refreshButton: ButtonCommandView;
    readonly addButton: ButtonCommandView;

    constructor(container: BasicComponentView) {
        super(container);
        this.layout();
        this.height100();
        this.setTemplateRows(CssLengthUnit.flex(1), CssLengthUnit.auto());
        const mainContent = this.addCell()
            .configure(c => {
                c.scrollable();
                c.positionRelative();
            })
            .addView(BlockView)
            .configure(c => {
                c.positionAbsoluteFill();
                c.scrollable();
            })
            .addView(BlockView)
            .configure(b => {
                b.setPadding(PaddingCss.top(3));
                b.addCssName('container');
            });
        this.alert = mainContent.addView(MessageAlertView);
        this.userGroups = mainContent
            .addView(LinkListGroupView);
        this.userGroups.setItemViewType(TextLinkListGroupItemView);
        const toolbar = HubTheme.instance.commandToolbar.toolbar(
            this.addCell().addView(ToolbarView)
        );
        this.menuButton = HubTheme.instance.commandToolbar.menuButton(
            toolbar.columnStart.addView(ButtonCommandView)
        );
        this.menuButton.setMargin(MarginCss.end(1));
        this.refreshButton = HubTheme.instance.commandToolbar.refreshButton(
            toolbar.columnStart.addView(ButtonCommandView)
        );
        this.addButton = HubTheme.instance.commandToolbar.addButton(
            toolbar.columnEnd.addView(ButtonCommandView)
        );
    }
}