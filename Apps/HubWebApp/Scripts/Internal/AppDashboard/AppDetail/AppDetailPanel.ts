import { AppComponent } from './AppComponent';
import { CurrentVersionComponent } from './CurrentVersionComponent';
import { ResourceGroupListCard } from './ResourceGroupListCard';
import { MostRecentErrorEventListCard } from './MostRecentErrorEventListCard';
import { ModifierCategoryListCard } from './ModifierCategoryListCard';
import { AppDetailPanelViewModel } from './AppDetailPanelViewModel';
import { HubAppApi } from '../../../Hub/Api/HubAppApi';
import { Awaitable } from 'XtiShared/Awaitable';
import { Command } from 'XtiShared/Command';
import { Result } from 'XtiShared/Result';
import { MostRecentRequestListCard } from './MostRecentRequestListCard';

export class AppDetailPanel {
    public static readonly ResultKeys = {
        backRequested: 'back-requested',
        resourceGroupSelected: 'resource-group-selected',
        modCategorySelected: 'mod-category-selected'
    };

    constructor(
        private readonly vm: AppDetailPanelViewModel,
        private readonly hubApi: HubAppApi
    ) {
        let backIcon = this.backCommand.icon();
        backIcon.setName('fa-caret-left');
        this.backCommand.setText('Apps');
        this.backCommand.setTitle('Back');
        this.backCommand.makeLight();
        this.resourceGroupListCard.resourceGroupSelected.register(
            this.onResourceGroupSelected.bind(this)
        );
        this.modifierCategoryListCard.modCategorySelected.register(
            this.onModCategorySelected.bind(this)
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

    private readonly app = new AppComponent(this.vm.app, this.hubApi);
    private readonly currentVersion = new CurrentVersionComponent(this.vm.currentVersion, this.hubApi);
    private readonly resourceGroupListCard = new ResourceGroupListCard(
        this.vm.resourceGroupListCard,
        this.hubApi
    );
    private readonly modifierCategoryListCard = new ModifierCategoryListCard(
        this.vm.modifierCategoryListCard,
        this.hubApi
    );
    private readonly mostRecentRequestListCard = new MostRecentRequestListCard(
        this.vm.mostRecentRequestListCard,
        this.hubApi
    );
    private readonly mostRecentErrorEventListCard = new MostRecentErrorEventListCard(
        this.vm.mostRecentErrorEventListCard,
        this.hubApi
    );

    private readonly awaitable = new Awaitable();

    private readonly backCommand = new Command(this.vm.backCommand, this.back.bind(this));

    private back() {
        this.awaitable.resolve(new Result(AppDetailPanel.ResultKeys.backRequested));
    }

    start() {
        return this.awaitable.start();
    }
}