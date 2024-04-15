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
import { MessageAlert } from '@jasonbenfield/sharedwebapp/Components/MessageAlert';
import { TextComponent } from '@jasonbenfield/sharedwebapp/Components/TextComponent';
import { CardAlert } from '@jasonbenfield/sharedwebapp/Components/CardAlert';

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
    private readonly appOptionsAlert: MessageAlert;
    private readonly appOptionsTextComponent: TextComponent;
    private readonly optionsAlert: MessageAlert;
    private readonly optionsTextComponent: TextComponent;
    private readonly resourceGroupListCard: ResourceGroupListCard;
    private readonly modifierCategoryListCard: ModifierCategoryListCard;
    private readonly mostRecentRequestListCard: MostRecentRequestListCard;
    private readonly mostRecentErrorEventListCard: MostRecentErrorEventListCard;

    private readonly awaitable = new Awaitable<Result>();

    private readonly backCommand = new Command(this.back.bind(this));

    constructor(
        private readonly hubClient: HubAppClient,
        private readonly view: AppDetailPanelView
    ) {
        this.app = new AppComponent(hubClient, view.app);
        this.currentVersion = new CurrentVersionComponent(hubClient, view.currentVersion);
        this.appOptionsAlert = new CardAlert(view.appOptionsAlertView).alert;
        this.appOptionsTextComponent = new TextComponent(view.appOptionsTextView);
        this.optionsAlert = new CardAlert(view.optionsAlertView).alert;
        this.optionsTextComponent = new TextComponent(view.optionsTextView);
        this.resourceGroupListCard = new ResourceGroupListCard(hubClient, view.resourceGroupListCard);
        this.resourceGroupListCard.when.resourceGroupClicked.then(
            this.onResourceGroupSelected.bind(this)
        );
        this.modifierCategoryListCard = new ModifierCategoryListCard(hubClient, view.modifierCategoryListCard);
        this.modifierCategoryListCard.when.modCategorySelected.then(
            this.onModCategorySelected.bind(this)
        );
        this.mostRecentRequestListCard = new MostRecentRequestListCard(hubClient, view.mostRecentRequestListCard);
        this.mostRecentErrorEventListCard = new MostRecentErrorEventListCard(hubClient, view.mostRecentErrorEventListCard);

        this.backCommand.add(view.backButton);
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
        const promises: Promise<any>[] = [
            this.app.refresh(),
            this.currentVersion.refresh(),
            this.refreshDefaultAppOptions(),
            this.refreshDefaultOptions(),
            this.resourceGroupListCard.refresh(),
            this.modifierCategoryListCard.refresh(),
            this.mostRecentRequestListCard.refresh(),
            this.mostRecentErrorEventListCard.refresh()
        ];
        return Promise.all(promises);
    }

    private async refreshDefaultAppOptions() {
        this.appOptionsTextComponent.setText('');
        this.view.showAppOptions();
        const defaultAppOptions = await this.appOptionsAlert.infoAction(
            'Loading...',
            () => this.hubClient.App.GetDefaultAppOptions()
        );
        if (defaultAppOptions) {
            const stringified = JSON.stringify(JSON.parse(defaultAppOptions), undefined, 2);
            this.appOptionsTextComponent.setText(stringified);
        }
        else {
            this.view.hideAppOptions();
        }
    }

    private async refreshDefaultOptions() {
        this.optionsTextComponent.setText('');
        this.view.showAppOptions();
        const defaultOptions = await this.optionsAlert.infoAction(
            'Loading...',
            () => this.hubClient.App.GetDefaultOptions()
        );
        if (defaultOptions) {
            const stringified = JSON.stringify(JSON.parse(defaultOptions), undefined, 2);
            console.log(stringified);
            this.optionsTextComponent.setText(stringified);
        }
        else {
            this.view.hideOptions();
        }
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