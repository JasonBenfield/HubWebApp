import { ButtonCommandItem } from "@jasonbenfield/sharedwebapp/Command/ButtonCommandItem";
import { Block } from "@jasonbenfield/sharedwebapp/Html/Block";
import { FlexColumn } from "@jasonbenfield/sharedwebapp/Html/FlexColumn";
import { FlexColumnFill } from "@jasonbenfield/sharedwebapp/Html/FlexColumnFill";
import { MarginCss } from "@jasonbenfield/sharedwebapp/MarginCss";
import { HubTheme } from "../../HubTheme";
import { MostRecentErrorEventListCardView } from "../MostRecentErrorEventListCardView";
import { MostRecentRequestListCardView } from "../MostRecentRequestListCardView";
import { ResourceAccessCardView } from "../ResourceAccessCardView";
import { ModCategoryComponentView } from "./ModCategoryComponentView";
import { ResourceGroupComponentView } from "./ResourceGroupComponentView";
import { ResourceListCardView } from "./ResourceListCardView";

export class ResourceGroupPanelView extends Block {
    readonly resourceGroupComponent: ResourceGroupComponentView;
    readonly modCategoryComponent: ModCategoryComponentView;
    readonly roleAccessCard: ResourceAccessCardView;
    readonly resourceListCard: ResourceListCardView;
    readonly mostRecentRequestListCard: MostRecentRequestListCardView;
    readonly mostRecentErrorEventListCard: MostRecentErrorEventListCardView;
    readonly backButton: ButtonCommandItem;

    constructor() {
        super();
        this.height100();
        let flexColumn = this.addContent(new FlexColumn());
        let flexFill = flexColumn.addContent(new FlexColumnFill());
        this.resourceGroupComponent = flexFill.addContent(new ResourceGroupComponentView())
            .configure(b => b.setMargin(MarginCss.bottom(3)));
        this.modCategoryComponent = flexFill.addContent(new ModCategoryComponentView())
            .configure(b => b.setMargin(MarginCss.bottom(3)));
        this.roleAccessCard = flexFill.addContent(new ResourceAccessCardView())
            .configure(b => b.setMargin(MarginCss.bottom(3)));
        this.resourceListCard = flexFill.addContent(new ResourceListCardView())
            .configure(b => b.setMargin(MarginCss.bottom(3)));
        this.mostRecentRequestListCard = flexFill.addContent(new MostRecentRequestListCardView())
            .configure(b => b.setMargin(MarginCss.bottom(3)));
        this.mostRecentErrorEventListCard = flexFill.addContent(
            new MostRecentErrorEventListCardView()
        ).configure(b => b.setMargin(MarginCss.bottom(3)));
        let toolbar = flexColumn.addContent(HubTheme.instance.commandToolbar.toolbar());
        this.backButton = toolbar.columnStart.addContent(HubTheme.instance.commandToolbar.backButton());
        this.backButton.setText('App');
    }
}