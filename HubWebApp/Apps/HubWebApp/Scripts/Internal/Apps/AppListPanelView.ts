import { CssLengthUnit } from "@jasonbenfield/sharedwebapp/CssLengthUnit";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";
import { GridView } from "@jasonbenfield/sharedwebapp/Views/Grid";
import { ButtonCommandView } from "../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Views/Command";
import { ToolbarView } from "../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Views/ToolbarView";
import { HubTheme } from "../HubTheme";
import { AppListCardView } from "./AppListCardView";

export class AppListPanelView extends GridView {
    readonly appListCard: AppListCardView;
    readonly menuButton: ButtonCommandView;

    constructor(container: BasicComponentView) {
        super(container);
        this.height100();
        this.layout();
        this.setTemplateRows(CssLengthUnit.flex(1), CssLengthUnit.auto());
        const mainContent = HubTheme.instance.mainContent(this.addCell());
        this.appListCard = mainContent.addView(AppListCardView);
        const toolbar = HubTheme.instance.commandToolbar.toolbar(this.addView(ToolbarView));
        this.menuButton = HubTheme.instance.commandToolbar.menuButton(
            toolbar.columnStart.addView(ButtonCommandView)
        );
    }
}