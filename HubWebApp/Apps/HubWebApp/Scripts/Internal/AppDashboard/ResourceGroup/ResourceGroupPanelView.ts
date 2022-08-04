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
import { ModCategoryComponentView } from "./ModCategoryComponentView";
import { ResourceGroupComponentView } from "./ResourceGroupComponentView";
import { ResourceListCardView } from "./ResourceListCardView";

export class ResourceGroupPanelView extends GridView {
    readonly resourceGroupComponent: ResourceGroupComponentView;
    readonly modCategoryComponent: ModCategoryComponentView;
    readonly roleAccessCard: ResourceAccessCardView;
    readonly resourceListCard: ResourceListCardView;
    readonly mostRecentRequestListCard: MostRecentRequestListCardView;
    readonly mostRecentErrorEventListCard: MostRecentErrorEventListCardView;
    readonly backButton: ButtonCommandView;

    constructor(container: BasicComponentView) {
        super(container);
        this.height100();
        this.layout();
        this.setTemplateRows(CssLengthUnit.flex(1), CssLengthUnit.auto());
        const mainContent = HubTheme.instance.mainContent(this.addCell());
        this.resourceGroupComponent = mainContent.addView(ResourceGroupComponentView)
            .configure(b => b.setMargin(MarginCss.bottom(3)));
        this.modCategoryComponent = mainContent.addView(ModCategoryComponentView)
            .configure(b => b.setMargin(MarginCss.bottom(3)));
        this.roleAccessCard = mainContent.addView(ResourceAccessCardView)
            .configure(b => b.setMargin(MarginCss.bottom(3)));
        this.resourceListCard = mainContent.addView(ResourceListCardView)
            .configure(b => b.setMargin(MarginCss.bottom(3)));
        this.mostRecentRequestListCard = mainContent.addView(MostRecentRequestListCardView)
            .configure(b => b.setMargin(MarginCss.bottom(3)));
        this.mostRecentErrorEventListCard = mainContent.addView(MostRecentErrorEventListCardView)
            .configure(b => b.setMargin(MarginCss.bottom(3)));
        const toolbar = this.addView(ToolbarView);
        HubTheme.instance.commandToolbar.toolbar(toolbar);
        this.backButton = toolbar.columnStart.addView(ButtonCommandView);
        HubTheme.instance.commandToolbar.backButton(this.backButton);
        this.backButton.setText('App');
    }
}