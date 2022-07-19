import { CssLengthUnit } from "../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/CssLengthUnit";
import { PaddingCss } from "../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/PaddingCss";
import { BasicComponentView } from "../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Views/BasicComponentView";
import { BlockView } from "../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Views/BlockView";
import { ButtonCommandView } from "../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Views/Command";
import { GridView } from "../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Views/Grid";
import { LinkListGroupView, TextLinkListGroupItemView } from "../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Views/ListGroup";
import { MessageAlertView } from "../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Views/MessageAlertView";
import { ToolbarView } from "../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Views/ToolbarView";
import { HubTheme } from "../HubTheme";

export class UserGroupsPanelView extends GridView {
    readonly alert: MessageAlertView;
    readonly userGroups: LinkListGroupView;
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
        const toolbar = this.addCell().addView(ToolbarView);
        HubTheme.instance.commandToolbar.toolbar(toolbar);
        this.refreshButton = toolbar.columnStart.addView(ButtonCommandView);
        HubTheme.instance.commandToolbar.refreshButton(this.refreshButton);
        this.addButton = toolbar.columnEnd.addView(ButtonCommandView);
        HubTheme.instance.commandToolbar.addButton(this.addButton);
    }
}