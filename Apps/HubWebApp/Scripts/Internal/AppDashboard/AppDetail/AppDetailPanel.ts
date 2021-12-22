import { Awaitable } from '@jasonbenfield/sharedwebapp/Awaitable';
import { Command } from '@jasonbenfield/sharedwebapp/Command/Command';
import { HubAppApi } from '../../../Hub/Api/HubAppApi';
import { AppComponent } from './AppComponent';
import { AppDetailPanelView } from './AppDetailPanelView';
import { CurrentVersionComponent } from './CurrentVersionComponent';
import { ModifierCategoryListCard } from './ModifierCategoryListCard';
import { MostRecentErrorEventListCard } from './MostRecentErrorEventListCard';
import { MostRecentRequestListCard } from './MostRecentRequestListCard';
import { ResourceGroupListCard } from './ResourceGroupListCard';

interface Results {
    backRequested?: {};
    resourceGroupSelected?: { resourceGroup: IResourceGroupModel; };
    modCategorySelected?: { modCategory: IModifierCategoryModel; };
}

export class AppDetailPanelResult {
    static get backRequested() {
        return new AppDetailPanelResult({ backRequested: {} });
    }

    static resourceGroupSelected(resourceGroup: IResourceGroupModel) {
        return new AppDetailPanelResult({
            resourceGroupSelected: { resourceGroup: resourceGroup }
        });
    }

    static modCategorySelected(modCategory: IModifierCategoryModel) {
        return new AppDetailPanelResult({
            modCategorySelected: { modCategory: modCategory }
        });
    }

    private constructor(private readonly results: Results) {
    }

    get backRequested() { return this.results.backRequested; }

    get resourceGroupSelected() { return this.results.resourceGroupSelected; }

    get modCategorySelected() { return this.results.modCategorySelected; }
}

export class AppDetailPanel implements IPanel {
    private readonly app: AppComponent;
    private readonly currentVersion: CurrentVersionComponent;
    private readonly resourceGroupListCard: ResourceGroupListCard;
    private readonly modifierCategoryListCard: ModifierCategoryListCard;
    private readonly mostRecentRequestListCard: MostRecentRequestListCard;
    private readonly mostRecentErrorEventListCard: MostRecentErrorEventListCard;

    private readonly awaitable = new Awaitable<AppDetailPanelResult>();

    private readonly backCommand = new Command(this.back.bind(this));

    constructor(
        private readonly hubApi: HubAppApi,
        private readonly view: AppDetailPanelView
    ) {
        this.app = new AppComponent(this.hubApi, this.view.app);
        this.currentVersion = new CurrentVersionComponent(this.hubApi, this.view.currentVersion);
        this.resourceGroupListCard = new ResourceGroupListCard(this.hubApi, this.view.resourceGroupListCard);
        this.resourceGroupListCard.resourceGroupSelected.register(
            this.onResourceGroupSelected.bind(this)
        );
        this.modifierCategoryListCard = new ModifierCategoryListCard(this.hubApi, this.view.modifierCategoryListCard);
        this.modifierCategoryListCard.modCategorySelected.register(
            this.onModCategorySelected.bind(this)
        );
        this.mostRecentRequestListCard = new MostRecentRequestListCard(this.hubApi, this.view.mostRecentRequestListCard);
        this.mostRecentErrorEventListCard = new MostRecentErrorEventListCard(this.hubApi, this.view.mostRecentErrorEventListCard);

        this.backCommand.add(this.view.backButton);
    }

    private onResourceGroupSelected(resourceGroup: IResourceGroupModel) {
        this.awaitable.resolve(
            AppDetailPanelResult.resourceGroupSelected(resourceGroup)
        );
    }

    private onModCategorySelected(modCategory: IModifierCategoryModel) {
        this.awaitable.resolve(
            AppDetailPanelResult.modCategorySelected(modCategory)
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

    private back() {
        this.awaitable.resolve(AppDetailPanelResult.backRequested);
    }

    start() {
        return this.awaitable.start();
    }

    activate() { this.view.show(); }

    deactivate() { this.view.hide(); }
}