import { CardAlertView, CardTitleHeaderView, CardView } from "@jasonbenfield/sharedwebapp/Views/Card";
import { ButtonCommandView } from "@jasonbenfield/sharedwebapp/Views/Command";
import { ButtonListGroupView } from "@jasonbenfield/sharedwebapp/Views/ListGroup";
import { CssLengthUnit } from "@jasonbenfield/sharedwebapp/CssLengthUnit";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";
import { GridView } from "@jasonbenfield/sharedwebapp/Views/Grid";
import { ToolbarView } from "@jasonbenfield/sharedwebapp/Views/ToolbarView";
import { HubTheme } from "../HubTheme";
import { RoleButtonListItemView } from "./RoleButtonListItemView";

export class AddRolePanelView extends GridView {
    readonly titleHeader: CardTitleHeaderView;
    readonly alert: CardAlertView;
    readonly roles: ButtonListGroupView<RoleButtonListItemView>;
    readonly backButton: ButtonCommandView;

    constructor(container: BasicComponentView) {
        super(container);
        this.height100();
        this.styleAsLayout();
        this.setTemplateRows(CssLengthUnit.flex(1), CssLengthUnit.auto());
        const mainContent = HubTheme.instance.mainContent(this.addCell());
        const card = mainContent.addView(CardView);
        this.titleHeader = card.addCardTitleHeader();
        this.alert = card.addCardAlert();
        this.roles = card.addButtonListGroup(RoleButtonListItemView);
        const toolbar = this.addView(ToolbarView);
        HubTheme.instance.commandToolbar.toolbar(toolbar);
        this.backButton = toolbar.columnStart.addView(ButtonCommandView);
        HubTheme.instance.commandToolbar.backButton(this.backButton);
    }
}