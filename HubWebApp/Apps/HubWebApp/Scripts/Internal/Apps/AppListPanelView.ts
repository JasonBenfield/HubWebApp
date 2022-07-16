import { CssLengthUnit } from "@jasonbenfield/sharedwebapp/CssLengthUnit";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";
import { GridView } from "@jasonbenfield/sharedwebapp/Views/Grid";
import { HubTheme } from "../HubTheme";
import { AppListCardView } from "./AppListCardView";

export class AppListPanelView extends GridView {
    readonly appListCard: AppListCardView;

    constructor(container: BasicComponentView) {
        super(container);
        this.height100();
        this.layout();
        this.setTemplateRows(CssLengthUnit.flex(1));
        const mainContent = HubTheme.instance.mainContent(this.addCell());
        this.appListCard = mainContent.addView(AppListCardView);
    }
}