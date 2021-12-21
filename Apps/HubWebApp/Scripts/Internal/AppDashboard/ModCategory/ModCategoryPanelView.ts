import { ButtonCommandItem } from "@jasonbenfield/sharedwebapp/Command/ButtonCommandItem";
import { Block } from "@jasonbenfield/sharedwebapp/Html/Block";
import { FlexColumn } from "@jasonbenfield/sharedwebapp/Html/FlexColumn";
import { FlexColumnFill } from "@jasonbenfield/sharedwebapp/Html/FlexColumnFill";
import { MarginCss } from "@jasonbenfield/sharedwebapp/MarginCss";
import { HubTheme } from "../../HubTheme";
import { ModCategoryComponentView } from "./ModCategoryComponentView";
import { ModifierListCardView } from "./ModifierListCardView";
import { ResourceGroupListCardView } from "./ResourceGroupListCardView";

export class ModCategoryPanelView extends Block {
    readonly modCategoryComponent: ModCategoryComponentView;
    readonly modifierListCard: ModifierListCardView;
    readonly resourceGroupListCard: ResourceGroupListCardView;
    readonly backButton: ButtonCommandItem;

    constructor() {
        super();
        this.height100();
        let flexColumn = this.addContent(new FlexColumn());
        let flexFill = flexColumn.addContent(new FlexColumnFill());
        this.modCategoryComponent = flexFill
            .addContent(new ModCategoryComponentView())
            .configure(b => b.setMargin(MarginCss.bottom(3)));
        this.modifierListCard = flexFill
            .addContent(new ModifierListCardView())
            .configure(b => b.setMargin(MarginCss.bottom(3)));
        this.resourceGroupListCard = flexFill
            .addContent(new ResourceGroupListCardView())
            .configure(b => b.setMargin(MarginCss.bottom(3)));
        let toolbar = flexColumn.addContent(HubTheme.instance.commandToolbar.toolbar());
        this.backButton = toolbar.columnStart.addContent(
            HubTheme.instance.commandToolbar.backButton()
        );
        this.backButton.setText('App');
    }
}