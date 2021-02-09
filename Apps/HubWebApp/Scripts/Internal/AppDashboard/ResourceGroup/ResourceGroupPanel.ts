import { Awaitable } from "XtiShared/Awaitable";
import { Result } from "XtiShared/Result";
import { Command } from "XtiShared/Command/Command";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { ResourceGroupAccessCard } from "./ResourceGroupAccessCard";
import { ResourceGroupComponent } from "./ResourceGroupComponent";
import { ResourceListCard } from "./ResourceListCard";
import { MostRecentRequestListCard } from "./MostRecentRequestListCard";
import { MostRecentErrorEventListCard } from "./MostRecentErrorEventListCard";
import { ModCategoryComponent } from "./ModCategoryComponent";
import { Block } from "XtiShared/Html/Block";
import { BlockViewModel } from "XtiShared/Html/BlockViewModel";
import { FlexColumn } from "XtiShared/Html/FlexColumn";
import { FlexColumnFill } from "XtiShared/Html/FlexColumnFill";
import { Toolbar } from "XtiShared/Html/Toolbar";
import { ButtonCommandItem } from "XtiShared/Command/ButtonCommandItem";
import { ContextualClass } from "XtiShared/ContextualClass";
import { MarginCss } from "XtiShared/MarginCss";
import { PaddingCss } from "XtiShared/PaddingCss";
import { HubTheme } from "../../HubTheme";

export class ResourceGroupPanel extends Block {
    public static readonly ResultKeys = {
        backRequested: 'back-requested',
        resourceSelected: 'resource-selected',
        modCategorySelected: 'mod-category-selected'
    };

    constructor(
        private readonly hubApi: HubAppApi,
        vm: BlockViewModel = new BlockViewModel()
    ) {
        super(vm);
        this.height100();
        let flexColumn = this.addContent(new FlexColumn());
        let flexFill = flexColumn.addContent(new FlexColumnFill());
        this.resourceGroupComponent = flexFill.addContent(new ResourceGroupComponent(this.hubApi))
            .configure(b => b.setMargin(MarginCss.bottom(3)));
        this.modCategoryComponent = flexFill.addContent(new ModCategoryComponent(this.hubApi))
            .configure(b => b.setMargin(MarginCss.bottom(3)));
        this.modCategoryComponent.clicked.register(
            this.onModCategoryClicked.bind(this)
        );
        this.roleAccessCard = flexFill.addContent(new ResourceGroupAccessCard(this.hubApi))
            .configure(b => b.setMargin(MarginCss.bottom(3)));
        this.resourceListCard = flexFill.addContent(new ResourceListCard(this.hubApi))
            .configure(b => b.setMargin(MarginCss.bottom(3)));
        this.resourceListCard.resourceSelected.register(this.onResourceSelected.bind(this));
        this.mostRecentRequestListCard = flexFill.addContent(new MostRecentRequestListCard(this.hubApi))
            .configure(b => b.setMargin(MarginCss.bottom(3)));
        this.mostRecentErrorEventListCard = flexFill.addContent(
            new MostRecentErrorEventListCard(this.hubApi)
        ).configure(b => b.setMargin(MarginCss.bottom(3)));

        let toolbar = flexColumn.addContent(HubTheme.instance.commandToolbar.toolbar());
        this.backCommand.add(
            toolbar.columnStart.addContent(HubTheme.instance.commandToolbar.backButton())
        ).configure(b => b.setText('App'));
    }

    private onModCategoryClicked(modCategory: IModifierCategoryModel) {
        this.awaitable.resolve(
            new Result(ResourceGroupPanel.ResultKeys.modCategorySelected, modCategory)
        );
    }

    private onResourceSelected(resource: IResourceModel) {
        this.awaitable.resolve(new Result(ResourceGroupPanel.ResultKeys.resourceSelected, resource));
    }

    setGroupID(groupID: number) {
        this.resourceGroupComponent.setGroupID(groupID);
        this.modCategoryComponent.setGroupID(groupID);
        this.roleAccessCard.setGroupID(groupID);
        this.resourceListCard.setGroupID(groupID);
        this.mostRecentRequestListCard.setGroupID(groupID);
        this.mostRecentErrorEventListCard.setGroupID(groupID);
    }

    private readonly resourceGroupComponent: ResourceGroupComponent;
    private readonly modCategoryComponent: ModCategoryComponent;
    private readonly roleAccessCard: ResourceGroupAccessCard;
    private readonly resourceListCard: ResourceListCard;
    private readonly mostRecentRequestListCard: MostRecentRequestListCard;
    private readonly mostRecentErrorEventListCard: MostRecentErrorEventListCard;

    async refresh() {
        let tasks: Promise<any>[] = [
            this.resourceGroupComponent.refresh(),
            this.modCategoryComponent.refresh(),
            this.roleAccessCard.refresh(),
            this.resourceListCard.refresh(),
            this.mostRecentRequestListCard.refresh(),
            this.mostRecentErrorEventListCard.refresh()
        ];
        return Promise.all(tasks);
    }

    private readonly awaitable = new Awaitable();

    start() {
        return this.awaitable.start();
    }

    private readonly backCommand = new Command(this.back.bind(this));

    private back() {
        this.awaitable.resolve(new Result(ResourceGroupPanel.ResultKeys.backRequested));
    }
}