import { Awaitable } from "XtiShared/Awaitable";
import { Command } from "XtiShared/Command/Command";
import { Result } from "XtiShared/Result";
import { Block } from "XtiShared/Html/Block";
import { BlockViewModel } from "XtiShared/Html/BlockViewModel";
import { FlexColumn } from "XtiShared/Html/FlexColumn";
import { FlexColumnFill } from "XtiShared/Html/FlexColumnFill";
import { MarginCss } from "XtiShared/MarginCss";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { ModCategoryComponent } from "./ModCategoryComponent";
import { ModifierListCard } from "./ModifierListCard";
import { ResourceGroupListCard } from "./ResourceGroupListCard";
import { HubTheme } from "../../HubTheme";

export class ModCategoryPanel extends Block {
    public static readonly ResultKeys = {
        backRequested: 'back-requested',
        resourceGroupSelected: 'resource-group-selected'
    };

    constructor(
        private readonly hubApi: HubAppApi,
        vm: BlockViewModel = new BlockViewModel()
    ) {
        super(vm);
        this.height100();
        let flexColumn = this.addContent(new FlexColumn());
        let flexFill = flexColumn.addContent(new FlexColumnFill());
        this.modCategoryComponent = flexFill
            .addContent(new ModCategoryComponent(this.hubApi))
            .configure(b => b.setMargin(MarginCss.bottom(3)));
        this.modifierListCard = flexFill
            .addContent(new ModifierListCard(this.hubApi))
            .configure(b => b.setMargin(MarginCss.bottom(3)));
        this.resourceGroupListCard = flexFill
            .addContent(new ResourceGroupListCard(this.hubApi))
            .configure(b => b.setMargin(MarginCss.bottom(3)));
        let toolbar = flexColumn.addContent(HubTheme.instance.commandToolbar.toolbar());
        this.backCommand.add(
            toolbar.columnStart.addContent(HubTheme.instance.commandToolbar.backButton())
        ).configure(b => b.setText('App'));
        this.resourceGroupListCard.resourceGroupSelected.register(
            this.onResourceGroupSelected.bind(this)
        );
    }

    private onResourceGroupSelected(resourceGroup: IResourceGroupModel) {
        this.awaitable.resolve(
            new Result(ModCategoryPanel.ResultKeys.resourceGroupSelected, resourceGroup)
        );
    }

    private readonly modCategoryComponent: ModCategoryComponent;
    private readonly modifierListCard: ModifierListCard;
    private readonly resourceGroupListCard: ResourceGroupListCard;

    setModCategoryID(categoryID: number) {
        this.modCategoryComponent.setModCategoryID(categoryID);
        this.modifierListCard.setModCategoryID(categoryID);
        this.resourceGroupListCard.setModCategoryID(categoryID);
    }

    refresh() {
        let promises: Promise<any>[] = [
            this.modCategoryComponent.refresh(),
            this.modifierListCard.refresh(),
            this.resourceGroupListCard.refresh()
        ];
        return Promise.all(promises);
    }

    private readonly awaitable = new Awaitable();

    start() {
        return this.awaitable.start();
    }

    readonly backCommand = new Command(this.back.bind(this));

    private back() {
        this.awaitable.resolve(new Result(ModCategoryPanel.ResultKeys.backRequested));
    }
}