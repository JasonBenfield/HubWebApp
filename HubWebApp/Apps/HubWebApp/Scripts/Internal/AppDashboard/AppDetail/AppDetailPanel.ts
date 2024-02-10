import { Awaitable } from '@jasonbenfield/sharedwebapp/Awaitable';
import { Command } from '@jasonbenfield/sharedwebapp/Components/Command';
import { HubAppClient } from '../../../Lib/Http/HubAppClient';
import { ResourceGroupListItem } from '../ResourceGroupListItem';
import { AppComponent } from './AppComponent';
import { AppDetailPanelView } from './AppDetailPanelView';
import { CurrentVersionComponent } from './CurrentVersionComponent';
import { ModifierCategoryListCard } from './ModifierCategoryListCard';
import { MostRecentErrorEventListCard } from './MostRecentErrorEventListCard';
import { MostRecentRequestListCard } from './MostRecentRequestListCard';
import { ResourceGroupListCard } from './ResourceGroupListCard';
import { ModifierCategory } from '../../../Lib/ModifierCategory';
import { AppResourceGroup } from '../../../Lib/AppResourceGroup';

interface IResult {
    backRequested?: {};
    resourceGroupSelected?: { resourceGroup: AppResourceGroup; };
    modCategorySelected?: { modCategory: ModifierCategory; };
}

class Result {
    static backRequested() {
        return new Result({ backRequested: {} });
    }

    static resourceGroupSelected(resourceGroup: AppResourceGroup) {
        return new Result({
            resourceGroupSelected: { resourceGroup: resourceGroup }
        });
    }

    static modCategorySelected(modCategory: ModifierCategory) {
        return new Result({
            modCategorySelected: { modCategory: modCategory }
        });
    }

    private constructor(private readonly results: IResult) {
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

    private readonly awaitable = new Awaitable<Result>();

    private readonly backCommand = new Command(this.back.bind(this));

    constructor(
        private readonly hubApi: HubAppClient,
        private readonly view: AppDetailPanelView
    ) {
        this.app = new AppComponent(this.hubApi, this.view.app);
        this.currentVersion = new CurrentVersionComponent(this.hubApi, this.view.currentVersion);
        this.resourceGroupListCard = new ResourceGroupListCard(this.hubApi, this.view.resourceGroupListCard);
        this.resourceGroupListCard.when.resourceGroupClicked.then(
            this.onResourceGroupSelected.bind(this)
        );
        this.modifierCategoryListCard = new ModifierCategoryListCard(this.hubApi, this.view.modifierCategoryListCard);
        this.modifierCategoryListCard.when.modCategorySelected.then(
            this.onModCategorySelected.bind(this)
        );
        this.mostRecentRequestListCard = new MostRecentRequestListCard(this.hubApi, this.view.mostRecentRequestListCard);
        this.mostRecentErrorEventListCard = new MostRecentErrorEventListCard(this.hubApi, this.view.mostRecentErrorEventListCard);

        this.backCommand.add(this.view.backButton);
    }

    private onResourceGroupSelected(listItem: ResourceGroupListItem) {
        this.awaitable.resolve(
            Result.resourceGroupSelected(listItem.group)
        );
    }

    private onModCategorySelected(modCategory: ModifierCategory) {
        this.awaitable.resolve(
            Result.modCategorySelected(modCategory)
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
        this.awaitable.resolve(Result.backRequested());
    }

    start() {
        return this.awaitable.start();
    }

    activate() { this.view.show(); }

    deactivate() { this.view.hide(); }
}