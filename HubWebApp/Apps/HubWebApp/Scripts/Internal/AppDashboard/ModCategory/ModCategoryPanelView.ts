import { MarginCss } from "@jasonbenfield/sharedwebapp/MarginCss";
import { CssLengthUnit } from "@jasonbenfield/sharedwebapp/CssLengthUnit";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";
import { ButtonCommandView } from "@jasonbenfield/sharedwebapp/Views/Command";
import { GridView } from "@jasonbenfield/sharedwebapp/Views/Grid";
import { ToolbarView } from "@jasonbenfield/sharedwebapp/Views/ToolbarView";
import { HubTheme } from "../../HubTheme";
import { ModCategoryComponentView } from "./ModCategoryComponentView";
import { ModifierListCardView } from "./ModifierListCardView";
import { ResourceGroupListCardView } from "./ResourceGroupListCardView";

export class ModCategoryPanelView extends GridView {
    readonly modCategoryComponent: ModCategoryComponentView;
    readonly modifierListCard: ModifierListCardView;
    readonly resourceGroupListCard: ResourceGroupListCardView;
    readonly backButton: ButtonCommandView;

    constructor(container: BasicComponentView) {
        super(container);
        this.height100();
        this.layout();
        this.setTemplateRows(CssLengthUnit.flex(1), CssLengthUnit.auto());
        const mainContent = HubTheme.instance.mainContent(this.addCell());
        this.modCategoryComponent = mainContent.addView(ModCategoryComponentView)
            .configure(b => b.setMargin(MarginCss.bottom(3)));
        this.modifierListCard = mainContent.addView(ModifierListCardView)
            .configure(b => b.setMargin(MarginCss.bottom(3)));
        this.resourceGroupListCard = mainContent.addView(ResourceGroupListCardView)
            .configure(b => b.setMargin(MarginCss.bottom(3)));
        const toolbar = this.addCell().addView(ToolbarView);
        HubTheme.instance.commandToolbar.toolbar(toolbar);
        this.backButton = toolbar.columnStart.addView(ButtonCommandView);
        HubTheme.instance.commandToolbar.backButton(this.backButton);
        this.backButton.setText('App');
    }
}