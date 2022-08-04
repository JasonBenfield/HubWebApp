import { MarginCss } from "@jasonbenfield/sharedwebapp/MarginCss";
import { ButtonCommandView } from "@jasonbenfield/sharedwebapp/Views/Command";
import { ButtonListGroupView } from "@jasonbenfield/sharedwebapp/Views/ListGroup";
import { MessageAlertView } from "@jasonbenfield/sharedwebapp/Views/MessageAlertView";
import { CssLengthUnit } from "@jasonbenfield/sharedwebapp/CssLengthUnit";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";
import { GridView } from "@jasonbenfield/sharedwebapp/Views/Grid";
import { ToolbarView } from "@jasonbenfield/sharedwebapp/Views/ToolbarView";
import { HubTheme } from "../HubTheme";
import { ModifierButtonListItemView } from "./ModifierButtonListItemView";

export class SelectModifierPanelView extends GridView {
    readonly alert: MessageAlertView;
    readonly modifiers: ButtonListGroupView;
    readonly backButton: ButtonCommandView;

    constructor(container: BasicComponentView) {
        super(container);
        this.height100();
        this.layout();
        this.setTemplateRows(CssLengthUnit.flex(1), CssLengthUnit.auto());
        const mainContent = HubTheme.instance.mainContent(this.addCell());
        this.alert = mainContent.addView(MessageAlertView);
        this.modifiers = mainContent.addView(ButtonListGroupView);
        this.modifiers.setItemViewType(ModifierButtonListItemView);
        this.modifiers.setMargin(MarginCss.bottom(3));
        const toolbar = this.addView(ToolbarView);
        HubTheme.instance.commandToolbar.toolbar(toolbar);
        this.backButton = toolbar.columnStart.addView(ButtonCommandView);
        HubTheme.instance.commandToolbar.backButton(this.backButton);
    }
}