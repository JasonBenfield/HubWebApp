import { CssLengthUnit } from "../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/CssLengthUnit";
import { BasicComponentView } from "../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Views/BasicComponentView";
import { GridView } from "../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Views/Grid";
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