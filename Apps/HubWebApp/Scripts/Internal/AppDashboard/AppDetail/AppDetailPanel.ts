import { AppComponent } from './AppComponent';
import { CurrentVersionComponent } from './CurrentVersionComponent';
import { ResourceGroupListCard } from './ResourceGroupListCard';
import { MostRecentErrorEventListCard } from './MostRecentErrorEventListCard';
import { ModifierCategoryListCard } from './ModifierCategoryListCard';
import { HubAppApi } from '../../../Hub/Api/HubAppApi';
import { Awaitable } from 'XtiShared/Awaitable';
import { Command } from 'XtiShared/Command/Command';
import { Result } from 'XtiShared/Result';
import { MostRecentRequestListCard } from './MostRecentRequestListCard';
import { Block } from 'XtiShared/Html/Block';
import { BlockViewModel } from 'XtiShared/Html/BlockViewModel';
import { Toolbar } from 'XtiShared/Html/Toolbar';
import { FlexColumn } from 'XtiShared/Html/FlexColumn';
import { FlexColumnFill } from 'XtiShared/Html/FlexColumnFill';
import { ButtonCommandItem } from 'XtiShared/Command/ButtonCommandItem';
import { ContextualClass } from 'XtiShared/ContextualClass';
import { MarginCss } from 'XtiShared/MarginCss';
import { PaddingCss } from 'XtiShared/PaddingCss';
import { HubTheme } from '../../HubTheme';

export class AppDetailPanel extends Block {
    public static readonly ResultKeys = {
        backRequested: 'back-requested',
        resourceGroupSelected: 'resource-group-selected',
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
        this.app = flexFill
            .addContent(new AppComponent(this.hubApi))
            .configure(b => b.setMargin(MarginCss.bottom(3)));
        this.currentVersion = flexFill
            .addContent(new CurrentVersionComponent(this.hubApi))
            .configure(b => b.setMargin(MarginCss.bottom(3)));
        this.resourceGroupListCard = flexFill
            .addContent(new ResourceGroupListCard(this.hubApi))
            .configure(b => b.setMargin(MarginCss.bottom(3)));
        this.resourceGroupListCard.resourceGroupSelected.register(
            this.onResourceGroupSelected.bind(this)
        );
        this.modifierCategoryListCard = flexFill
            .addContent(new ModifierCategoryListCard(this.hubApi))
            .configure(b => b.setMargin(MarginCss.bottom(3)));
        this.modifierCategoryListCard.modCategorySelected.register(
            this.onModCategorySelected.bind(this)
        );
        this.mostRecentRequestListCard = flexFill
            .addContent(new MostRecentRequestListCard(this.hubApi))
            .configure(b => b.setMargin(MarginCss.bottom(3)));
        this.mostRecentErrorEventListCard = flexFill
            .addContent(new MostRecentErrorEventListCard(this.hubApi))
            .configure(b => b.setMargin(MarginCss.bottom(3)));

        let toolbar = flexColumn.addContent(HubTheme.instance.commandToolbar.toolbar());
        this.backCommand.add(
            toolbar.columnStart.addContent(HubTheme.instance.commandToolbar.backButton())
        );
    }

    private onResourceGroupSelected(resourceGroup: IResourceGroupModel) {
        this.awaitable.resolve(
            new Result(AppDetailPanel.ResultKeys.resourceGroupSelected, resourceGroup)
        );
    }

    private onModCategorySelected(modCategory: IModifierCategoryModel) {
        this.awaitable.resolve(
            new Result(AppDetailPanel.ResultKeys.modCategorySelected, modCategory)
        );
    }

    async refresh() {
        let promises: Promise<any>[] = [
            this.app.refresh(),
            this.currentVersion.refresh(),
            this.resourceGroupListCard.refresh(),
            this.modifierCategoryListCard.refresh(),
            this.mostRecentRequestListCard.refresh(),
            this.mostRecentErrorEventListCard.refresh()
        ];
        return Promise.all(promises);
    }

    private readonly app: AppComponent;
    private readonly currentVersion: CurrentVersionComponent;
    private readonly resourceGroupListCard: ResourceGroupListCard;
    private readonly modifierCategoryListCard: ModifierCategoryListCard;
    private readonly mostRecentRequestListCard: MostRecentRequestListCard;
    private readonly mostRecentErrorEventListCard: MostRecentErrorEventListCard;

    private readonly awaitable = new Awaitable();

    private readonly backCommand = new Command(this.back.bind(this));

    private back() {
        this.awaitable.resolve(new Result(AppDetailPanel.ResultKeys.backRequested));
    }

    start() {
        return this.awaitable.start();
    }
}