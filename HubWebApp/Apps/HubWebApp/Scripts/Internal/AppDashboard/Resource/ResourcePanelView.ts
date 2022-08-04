import { MarginCss } from "@jasonbenfield/sharedwebapp/MarginCss";
import { CssLengthUnit } from "@jasonbenfield/sharedwebapp/CssLengthUnit";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";
import { ButtonCommandView } from "@jasonbenfield/sharedwebapp/Views/Command";
import { GridView } from "@jasonbenfield/sharedwebapp/Views/Grid";
import { ToolbarView } from "@jasonbenfield/sharedwebapp/Views/ToolbarView";
import { HubTheme } from "../../HubTheme";
import { MostRecentErrorEventListCardView } from "../MostRecentErrorEventListCardView";
import { MostRecentRequestListCardView } from "../MostRecentRequestListCardView";
import { ResourceAccessCardView } from "../ResourceAccessCardView";
import { ResourceComponentView } from "./ResourceComponentView";

export class ResourcePanelView extends GridView {
    readonly resourceComponent: ResourceComponentView;
    readonly resourceAccessCard: ResourceAccessCardView;
    readonly mostRecentRequestListCard: MostRecentRequestListCardView;
    readonly mostRecentErrorEventListCard: MostRecentErrorEventListCardView;
    readonly backButton: ButtonCommandView;

    constructor(container: BasicComponentView) {
        super(container);
        this.height100();
        this.layout();
        this.setTemplateRows(CssLengthUnit.flex(1), CssLengthUnit.auto());
        const mainContent = HubTheme.instance.mainContent(this.addCell());
        this.resourceComponent = mainContent.addView(ResourceComponentView)
            .configure(b => b.setMargin(MarginCss.bottom(3)));
        this.resourceAccessCard = mainContent.addView(ResourceAccessCardView)
            .configure(b => b.setMargin(MarginCss.bottom(3)));
        this.mostRecentRequestListCard = mainContent.addView(MostRecentRequestListCardView)
            .configure(b => b.setMargin(MarginCss.bottom(3)));
        this.mostRecentErrorEventListCard = mainContent.addView(MostRecentErrorEventListCardView)
            .configure(b => b.setMargin(MarginCss.bottom(3)));
        const toolbar = this.addView(ToolbarView);
        HubTheme.instance.commandToolbar.toolbar(toolbar);
        this.backButton = toolbar.columnStart.addView(ButtonCommandView);
        HubTheme.instance.commandToolbar.backButton(this.backButton);
        this.backButton.setText('Resource Group');
    }
}