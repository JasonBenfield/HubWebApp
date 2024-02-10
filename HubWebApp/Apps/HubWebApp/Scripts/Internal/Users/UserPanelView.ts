import { CssLengthUnit } from "@jasonbenfield/sharedwebapp/CssLengthUnit";
import { MarginCss } from "@jasonbenfield/sharedwebapp/MarginCss";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";
import { ButtonCommandView } from "@jasonbenfield/sharedwebapp/Views/Command";
import { GridView } from "@jasonbenfield/sharedwebapp/Views/Grid";
import { ToolbarView } from "@jasonbenfield/sharedwebapp/Views/ToolbarView";
import { AppListCardView } from "../Apps/AppListCardView";
import { HubTheme } from "../HubTheme";
import { UserAuthenticatorListCardView } from "./UserAuthenticatorListCardView";
import { UserComponentView } from "./UserComponentView";

export class UserPanelView extends GridView {
    readonly userComponent: UserComponentView;
    readonly userAuthenticatorListCard: UserAuthenticatorListCardView;
    readonly appListCard: AppListCardView;
    readonly backButton: ButtonCommandView;

    constructor(container: BasicComponentView) {
        super(container);
        this.setViewName(UserPanelView.name);
        this.height100();
        this.styleAsLayout();
        this.setTemplateRows(CssLengthUnit.flex(1), CssLengthUnit.auto());
        const mainContent = HubTheme.instance.mainContent(this.addCell());
        this.userComponent = mainContent.addView(UserComponentView)
        this.userComponent.setMargin(MarginCss.bottom(3));
        this.userAuthenticatorListCard = mainContent.addView(UserAuthenticatorListCardView);
        this.userAuthenticatorListCard.setMargin(MarginCss.bottom(3));
        this.appListCard = mainContent.addView(AppListCardView);
        this.appListCard.setMargin(MarginCss.bottom(3));
        const toolbar = HubTheme.instance.commandToolbar.toolbar(this.addView(ToolbarView));
        this.backButton = HubTheme.instance.commandToolbar.backButton(
            toolbar.columnStart.addView(ButtonCommandView)
        );
        this.backButton.setText('Users');
    }
}